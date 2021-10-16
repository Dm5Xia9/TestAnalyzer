using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public class Analyzer<TService> : Analyzer where TService : BaseServiceInfo
    {
        [JsonProperty(PropertyName = "patient")]
        public string Patient { get; set; }

        [JsonProperty(PropertyName = "services")]
        public List<TService> Services { get; set; }
    }

    public class Analyzer
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public long AnalyzerId { get; set; }
    }
}
