using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace FunctionBlobTriggerApp
{
    public static class ResizeFunction
    {
        [FunctionName("ResizeFunction")]
        public static async Task Run([BlobTrigger("udemy-pictures/{name}", Connection = "MyAzureStorage")] Stream myBlob, string name, ILogger log, [Blob("udemy-pictures-resize/{name}", FileAccess.Write, Connection = "MyAzureStorage")] Stream outputBlobStream)
        {
            var format = await Image.DetectFormatAsync(myBlob);

            var ResizeImage = Image.Load(myBlob);

            ResizeImage.Mutate(x => x.Resize(100, 100));

            ResizeImage.Save(outputBlobStream, format);
            log.LogInformation($"Resim resize işlemi başarıyla gerçekleştirildi.");
        }
    }
}