using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models
{
    public class Analyzer : BaseRecord
    {
        public string Name { get; set; }

        public List<AnalyzerResult> AnalyzerResults { get; set; }
        public List<Study> Studies { get; set; }
    }
}
