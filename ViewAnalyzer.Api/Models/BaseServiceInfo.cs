using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewAnalyzer.Api.Models
{
    public abstract class BaseServiceInfo
    {
        public abstract int ServiceCode { get; set; }
    }
}
