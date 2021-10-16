using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewAnalyzer.Models;

namespace ViewAnalyzer.Api.Models
{
    public class ServiceInfoRequest : BaseServiceInfo
    {
        [JsonIgnore]
        public Study Study { get; set; }
    }
}
