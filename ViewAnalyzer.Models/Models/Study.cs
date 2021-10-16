using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models
{
    public class Study : BaseRecord
    {
        public string Name { get; set; }
        public int ServiceCode { get; set; }
        public TypeResult TypeResult { get; set; }
        public double Price { get; set; }

        public List<AnalyzerResult> AnalyzerResults { get; set; }
        public List<Analyzer> Analyzers { get; set; }
    }

    public enum TypeResult
    {
        Integer = 0,
        String,
    }
}
