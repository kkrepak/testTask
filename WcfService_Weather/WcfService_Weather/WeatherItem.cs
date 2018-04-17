using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService_Weather
{
    public class WeatherItem
    {
            public int WeatherItemId { get; set; }
            public string City { get; set; }
            public DateTime Date { get; set; }
            public double Temperature { get; set; }
            public double Pressure { get; set; }
       }

    public class SampleContext : DbContext
    {
        public SampleContext() : base("MyWeatherDB")
        { }

        public DbSet<WeatherItem> WeatherItems { get; set; }
    }
}