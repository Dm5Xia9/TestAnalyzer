﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public class ServiceResult
    {
        public ProgressInfo ProgressInfo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsOk => StatusCode == HttpStatusCode.OK;

        public Analyzer<ServiceInfoResult> Result { get; set; }
    }

    public class ProgressInfo
    {
        [JsonProperty(PropertyName = "progress")]
        public int Progress { get; set; }
    }
}
