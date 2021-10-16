using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public class ServiceInfoResult : BaseServiceInfo
    {
        [JsonPropertyName("result")]
        public JsonElement Result { get; set; }

        public string GetString()
        {
            return Result.ValueKind == JsonValueKind.String ? Result.GetString() : Result.GetDouble().ToString();
        }
    }
}
