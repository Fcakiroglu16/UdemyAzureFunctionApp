using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionTimerTriggerApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("* * * * * *")] TimerInfo myTimer, ILogger log, [Blob("logs/{rand-guid}.txt", System.IO.FileAccess.Write, Connection = "MyAzureStorage")] Stream blobStream)
        {
            var ifade = Encoding.UTF8.GetBytes($"loglama: {DateTime.Now}");

            blobStream.Write(ifade, 0, ifade.Length);
            blobStream.Close();
            blobStream.Dispose();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}