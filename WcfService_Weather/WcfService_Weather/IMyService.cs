using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService_Weather
{
	// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IMyService" в коде и файле конфигурации.
	[ServiceContract]
	public interface IMyService
	{
		[OperationContract]
		[WebInvoke(Method = "GET",
			ResponseFormat = WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped,
			UriTemplate = "get/{id}")]
		ResponseData GETData(string id);

		[OperationContract]
		[WebInvoke(Method = "POST",
			ResponseFormat = WebMessageFormat.Json,
            RequestFormat =WebMessageFormat.Json,
			BodyStyle = WebMessageBodyStyle.Wrapped,
			UriTemplate = "post")]
		ResponseData POSTData(RequestData rData);
	}
}
