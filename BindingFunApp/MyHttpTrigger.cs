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

namespace BindingFunApp
{
    public static class MyHttpTrigger
    {
        [FunctionName("MyHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log, [Table("Products", Connection = "MyAzureStorage")] CloudTable cloudTable)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product newProduct = JsonConvert.DeserializeObject<Product>(requestBody);

            TableOperation operation = TableOperation.Insert(newProduct);

            await cloudTable.ExecuteAsync(operation);

            return new OkObjectResult(newProduct);
        }
    }
}