using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Net;

namespace WcfService_Weather
{
	public class MyService : IMyService
	{
		#region IMyService Members

		public ResponseData GETData(string city)
		{
			ResponseData rd = CheckCity(city);
			return rd;
		}
		public ResponseData POSTData(RequestData rData) // pass params through body instead of query params
		{
			return new ResponseData { Name = rData.name, Pressure = "OK", Temperature = "100" };
		}
		#endregion

		private ResponseData GetRequest(string city)
		{
			string URL = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=0de82b6b4ba5d843dac44bbee4d02543";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
			request.Method = "POST";
			request.ContentType = "application/json";
			StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
			requestWriter.Close();

			try
			{
				WebResponse webResponse = request.GetResponse();
				Stream webStream = webResponse.GetResponseStream();
				StreamReader responseReader = new StreamReader(webStream);
				string response = responseReader.ReadToEnd();
				CreateItem(response, DateTime.Now);
				Weather wi = JsonConvert.DeserializeObject<Weather>(response);
				return new ResponseData { Name = wi.name, Pressure = wi.main.pressure.ToString(), Temperature = wi.main.temp.ToString() };
			}
			catch (Exception e)
			{
				return null;
			}
		}

		internal class JavaScriptSerializer
		{
			public JavaScriptSerializer()
			{
			}
		}

		class Main
		{
			public double temp;
			public double pressure;
		}
		class Weather
		{
			public string name;
			public Main main;
		}

		protected void CreateItem(string response, DateTime time)
		{
			Weather weather = JsonConvert.DeserializeObject<Weather>(response);
			WeatherItem wth = new WeatherItem();
			wth.City = weather.name;
			wth.Date = time;
			wth.Pressure = weather.main.pressure;
			wth.Temperature = weather.main.temp;
			SampleContext context = new SampleContext();
			context.WeatherItems.Add(wth);
			context.SaveChanges();
		}

		protected ResponseData CheckCity(string city)
		{
			SampleContext context = new SampleContext();
			WeatherItem[] items = context.WeatherItems.ToArray();
			for (int i = items.Length - 1; i >= 0; i--)
			{
				if (items[i].City == city)
				{
					TimeSpan ts = DateTime.Now - items[i].Date;
					if (ts < new TimeSpan(0, 5, 0))
					{
						return new ResponseData { Name = items[i].City, Pressure = items[i].Pressure.ToString(), Temperature = items[i].Temperature.ToString() };
					}
				}
			}

			return GetRequest(city);

		}
	}
}

