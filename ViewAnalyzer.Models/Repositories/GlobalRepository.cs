using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewAnalyzer.Core.Data;

namespace ViewAnalyzer.Models.Repositories
{
    //так быстрее
    public class GlobalRepository : Repository
    {
        public GlobalRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<Analyzer> GetIncludeAnalyzers()
        {
            return Table<Analyzer>().Include(p => p.Studies).ThenInclude(p => p.AnalyzerResults).ToList();
        }

        public List<AnalyzerResult> GetResultByStudy(Study study, Analyzer analyzer)
        {
            return Table<AnalyzerResult>().Include(p => p.Studies).Include(p => p.Analyzer)
                .Where(p => p.Studies.Contains(study) && p.Analyzer == analyzer).OrderBy(p => p.CreatedAt).ToList();
        }

        public bool IsActiveAnalyzer(Analyzer analyzer)
        {
            return !Table<AnalyzerResult>().Where(p => p.AnalyzerId == analyzer.Id).Any(p => p.ResultState == ResultState.Process);
        }

    }
}
