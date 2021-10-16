using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public class ServiceInfoResult : BaseServiceInfo
    {
        [JsonProperty(PropertyName = "serviceCode")]
        public override int ServiceCode { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }
    }
}
