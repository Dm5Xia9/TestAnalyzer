using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Models;

namespace ViewAnalyzer.Api.Models
{
    public class ServiceInfoRequest : BaseServiceInfo
    {
        [JsonProperty(PropertyName = "serviceCode")]
        public override int ServiceCode { get; set; }

        [JsonIgnore]
        public Study Study { get; set; }
    }
}
