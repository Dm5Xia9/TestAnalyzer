using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewAnalyzer.Api.Models;
using ViewAnalyzer.Models;

namespace ViewAnalyzer.Api
{
    public class AnalyzerService
    {
        string baseUrl;
        public AnalyzerService(string host, string port)
        {
            baseUrl = $"http://{host}:{port}/api/analyzer";
        }

        public async Task<HttpResponseMessage> SendRequest(object obj, string serviceName, string method = "GET")
        {
            StringContent httpContent = null;
            if(obj != null)
            {
                var json = JsonConvert.SerializeObject(obj);

                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            using HttpClient httpClient = new HttpClient();

            using HttpRequestMessage httpRequest = new HttpRequestMessage(new HttpMethod(method), $"{baseUrl}/{serviceName}");

            httpRequest.Content = httpContent;

            return await httpClient.SendAsync(httpRequest);
        }

        public async Task<ServiceResult> SendService(Analyzer<ServiceInfoRequest> analyzer)
        {
            var response = await SendRequest(analyzer, analyzer.Name, "POST");
            return new ServiceResult
            {
                StatusCode = response.StatusCode
            };

        }

        public async Task SendWaitResult(Analyzer<ServiceInfoRequest> analyzer, DataFactory dataFactory)
        {
            var result = await SendService(analyzer);

            using var db = dataFactory.Create();

            var analyzerResult = new AnalyzerResult
            {
                AnalyzerId = analyzer.AnalyzerId,
                Patient = analyzer.Patient,
                ResultState = ResultState.Process,
                Progress = 0,
            };

            var studies = db.Studies.GetMany(analyzer.Services.Select(p => p.Study.Id));

            analyzerResult.Studies.AddRange(studies);

            db.AnalyzerResults.Insert(analyzerResult);
            db.SaveChanges();

            var isEnd = false;
            var i = 0;

            while (true)
            {

                if (i >= 100)
                {
                    analyzerResult.Error = HttpStatusCode.RequestTimeout.ToString();

                    analyzerResult.ResultState = ResultState.Expect;

                    isEnd = true;
                }

                var waitResult = await GetResult(analyzer);

                if(waitResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    analyzerResult.Error = waitResult.StatusCode.ToString();

                    analyzerResult.ResultState = ResultState.Expect;

                    isEnd = true;
                }
                else if(waitResult.ProgressInfo != null)
                {
                    analyzerResult.Progress = waitResult.ProgressInfo.Progress;
                }
                else
                {
                    analyzerResult.Result = waitResult.Result.Services.Select(p => 
                        new StudyResult { Code = p.ServiceCode, Result = p.Result }).ToList();

                    analyzerResult.ResultState = ResultState.Expect;

                    isEnd = true;
                }

                db.AnalyzerResults.Update(analyzerResult);
                db.SaveChanges();

                i++;

                if (isEnd)
                    break;
                else
                    Thread.Sleep(2000);

            }

        }

        private async Task<ServiceResult> GetResult(Models.Analyzer analyzer)
        {
            var response = await SendRequest(null, analyzer.Name);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ServiceResult
                {
                    StatusCode = response.StatusCode,
                };
            }


            var str = await response.Content.ReadAsStringAsync();

            if (str.Contains("progress"))
            {
                return new ServiceResult
                {
                    StatusCode = response.StatusCode,
                    ProgressInfo = JsonConvert.DeserializeObject<ProgressInfo>(str),
                };
            }

            return new ServiceResult
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Result = JsonConvert.DeserializeObject<Analyzer<ServiceInfoResult>>(str),
            };
        }


    }
}
