using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs.Host;

namespace BindingFunApp
{
    public static class MyHttpTrigger
    {
        [FunctionName("MyHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, [Table("Products", Connection = "MyAzureStorage")] CloudTable cloudTable, [Queue("queueproduct", Connection = "MyAzureStorage")] CloudQueue cloudQueue)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product newProduct = JsonConvert.DeserializeObject<Product>(requestBody);

            TableOperation operation = TableOperation.Insert(newProduct);

            await cloudTable.ExecuteAsync(operation);

            var productString = JsonConvert.SerializeObject(newProduct);

            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(productString);
            await cloudQueue.AddMessageAsync(cloudQueueMessage);

            return new OkObjectResult(newProduct);
        }
    }
}