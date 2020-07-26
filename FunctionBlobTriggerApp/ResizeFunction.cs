using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
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
        public static async Task Run([BlobTrigger("udemy-pictures/{name}", Connection = "MyAzureStorage")] Stream myBlob, string name, ILogger log, [Blob("udemy-pictures-resize", Connection = "MyAzureStorage")] CloudBlobContainer cloudBlobContainer)
        {
            await cloudBlobContainer.CreateIfNotExistsAsync();
            MemoryStream ms = new MemoryStream();
            var format = await Image.DetectFormatAsync(myBlob);

            var blockBLob = cloudBlobContainer.GetBlockBlobReference($"{Guid.NewGuid()}-100.{format.FileExtensions.First()}");
            ;
            var ResizeImage = Image.Load(myBlob);

            ResizeImage.Mutate(x => x.Resize(100, 100));

            ResizeImage.Save(ms, format);

            ms.Position = 0;

            blockBLob.UploadFromStream(ms);
            ms.Close();
            ms.Dispose();

            log.LogInformation($"Resim resize işlemi başarıyla gerçekleştirildi.");
        }
    }
}