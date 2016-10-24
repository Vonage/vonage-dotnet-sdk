using System;
using System.Linq;
using Nexmo.Api;
using Nexmo.Api.Voice;

namespace Nexmo.Samples.Coverage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApiRequestCoverageTest();

            //RemoveUnusedApplications();
        }

        /// <summary>
        /// This is a quick sanity check with 90%+ code coverage of the ApiRequest classes
        /// </summary>
        private static void ApiRequestCoverageTest()
        {
            Console.WriteLine("Account.GetBalance() = {0}", Account.GetBalance());
            Console.WriteLine("Account.GetNumbers() = {0}", Account.GetNumbers().count);

            /////

            var niResp = NumberInsight.Request(new NumberInsight.NumberInsightRequest
            {
                Number = Configuration.Instance.Settings["test_number"],
                Callback = "https://abcdefg.ngrok.io/ni/"
            });
            Console.WriteLine("NumberInsight.Request() = {0}", niResp.status);

            /////

            var appRequest = new ApplicationRequest
            {
                name = "coveragetest",
                type = "voice",
                answer_url = "https://abcdefg.ngrok.io/api/voice",
                event_url = "https://abcdefg.ngrok.io/api/voice",
            };

            var appResp = Application.Create(appRequest);
            Console.WriteLine("Application.Create() = {0}", appResp.id);

            var appList = Application.List();
            Console.WriteLine("Application.List() = {0}", appList.Count);
            var filteredAppList = Application.List(10, 0, appResp.id);
            Console.WriteLine("Application.List(10, 0, appResp.id) = {0}", filteredAppList.Count);
            appRequest.id = appResp.id;
            appRequest.name = "updcoveragetest";
            var appUpdateResp = Application.Update(appRequest);
            Console.WriteLine("Application.Update(appRequest) = {0}", appUpdateResp.name);
            var isDeleted = Application.Delete(appUpdateResp.id);
            Console.WriteLine("Application.Delete(id) = {0}", isDeleted);

            /////

            var results = Call.Do(new Call.CallCommand
            {
                to = new[]
                {
                    new Call.Endpoint
                    {
                        type = "phone",
                        number = Configuration.Instance.Settings["test_number"]
                    }
                },
                @from = new Call.Endpoint
                {
                    type = "phone",
                    number = Configuration.Instance.Settings["nexmo_number"]
                },
                answer_url = new[]
                {
                    "https://nexmo-community.github.io/ncco-examples/first_call_talk.json"
                }
            });
            Console.WriteLine("Call.Do() = {0}", results.status);
            var callListResults = Call.List();
            Console.WriteLine("Call.List() = {0}", callListResults.count);
        }

        private static void RemoveUnusedApplications()
        {
            var appList = Application.List();
            Console.WriteLine("BEFORE: Application.List() = {0}", appList.Count);
            foreach (var app in appList.Where(a => a.id != Configuration.Instance.Settings["Nexmo.Application.Id"]))
            {
                Console.WriteLine(app.id);
                Application.Delete(app.id);
            }
            Console.WriteLine("AFTER: Application.List() = {0}", Application.List().Count);
        }
    }
}

