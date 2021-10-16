using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public class Analyzer<TService> : Analyzer where TService : BaseServiceInfo
    {
        [JsonPropertyName("patient")]
        public string Patient { get; set; }

        [JsonPropertyName("services")]
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
