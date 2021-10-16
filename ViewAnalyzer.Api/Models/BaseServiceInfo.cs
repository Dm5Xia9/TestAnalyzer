using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public abstract class BaseServiceInfo
    {
        [JsonPropertyName("serviceCode")]
        public int ServiceCode { get; set; }
    }
}
