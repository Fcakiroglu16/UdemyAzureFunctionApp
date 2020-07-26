using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

namespace FunctionQueueTriggerApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("udemy-queue", Connection = "")] string message, ILogger log, [Blob("udemy-pictures/{queueTrigger}", System.IO.FileAccess.Read, Connection = "")] CloudBlockBlob cloudBlockBlob)
        {
            log.LogInformation("Blob  Name:" + cloudBlockBlob.Name);
            log.LogInformation("Blob  Tip:" + cloudBlockBlob.BlobType.ToString());
        }
    }
}