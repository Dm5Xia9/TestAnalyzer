using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models
{
    public class AnalyzerResult : BaseRecord
    {
        public string Patient { get; set; }

        [Column(TypeName = "jsonb")]
        public List<StudyResult> Result { get; set; }

        public ResultState ResultState { get; set; }
        public int Progress { get; set; }

        public string Error { get; set; }

        public long AnalyzerId { get; set; }
        public Analyzer Analyzer { get; set; }
        public long StudyId { get; set; }
        public List<Study> Studies { get; set; } = new List<Study>();

        public string GetStateText()
        {
            if (ResultState == ResultState.NoCompleted)
                return "Не одобрен";
            else if (ResultState == ResultState.Completed)
                return "Одобрен";
            else if (ResultState == ResultState.Expect)
                return "Ожидает";
            else
                return "Загружается";
        }
    }

    public enum ResultState
    {
        Completed = 0,
        NoCompleted,
        Expect,
        Process,
    }

    public class StudyResult
    {
        public int Code { get; set; }
        public string Result { get; set; }
    }
}
