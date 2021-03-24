using System;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QuizFunction
{
    public static class Create
    {
        [FunctionName("Create")]
        public static void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            // Call Your  API
            HttpClient newClient = new HttpClient();
            HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Get, string.Format("https://localhost:44395/api/quiz/create/{0}", 0));
            try
            {
                //Read Server Response
                newClient.SendAsync(newRequest);
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
