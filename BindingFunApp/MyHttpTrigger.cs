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
        [return: Queue("queueproduct", Connection = "MyAzureStorage")]
        public static async Task<Product> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Product newProduct = JsonConvert.DeserializeObject<Product>(requestBody);

            return newProduct;
        }
    }
}