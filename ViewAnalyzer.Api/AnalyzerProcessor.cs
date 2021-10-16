using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewAnalyzer.Api.Models;
using ViewAnalyzer.Models;

namespace ViewAnalyzer.Api
{
    public class AnalyzerProcessor
    {
        Thread processor;
        public AnalyzerProcessor(AnalyzerService analyzerService, DataFactory dataFactory)
        {
            this.processor = new Thread(async p =>
            {
                var analyzer = p as Analyzer<ServiceInfoRequest>;

                await analyzerService.SendWaitResult(analyzer, dataFactory);
            });
        }

        public void Start(Analyzer<ServiceInfoRequest> analyzer)
        {
            processor.Start(analyzer);
        }
    }
}
