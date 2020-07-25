using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionBlobTriggerApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([BlobTrigger("udemy-pictures/{name}.jpg", Connection = "")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}