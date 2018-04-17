using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfService_Weather
{
    [DataContract(Namespace = "WcfService_Weather")]
    public class RequestData
    {
        [DataMember]
        public string name { get; set; }
    }
    
    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Pressure { get; set; }

        [DataMember]
        public string Temperature { get; set; }
    }
}