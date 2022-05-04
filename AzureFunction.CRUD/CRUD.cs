using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunction.CRUD.Infrastructure;
using AzureFunction.CRUD.Entities;
using System.Net;
using System.Collections.Generic;

namespace AzureFunction.CRUD
{
    internal class CRUD
    {
        private readonly MongoDBService mongoDBService;
        public CRUD(MongoDBService mongoDBService)
        {
            this.mongoDBService = mongoDBService;
        }
        [FunctionName("Create")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            User data = JsonConvert.DeserializeObject<User>(requestBody);
            mongoDBService.Create(data);

            return new OkObjectResult(HttpStatusCode.OK);
        }
        [FunctionName("Read")]
        public async Task<User> Read(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string guid = req.Query["id"].ToString();
            Guid id = Guid.Parse(guid);

            return await mongoDBService.Read(id);
        }
        [FunctionName("List")]
        public async Task<List<User>> List(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await mongoDBService.Read();
        }
        [FunctionName("Update")]
        public async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            User data = JsonConvert.DeserializeObject<User>(requestBody);
            mongoDBService.Update(data);

            return new OkObjectResult(HttpStatusCode.OK);
        }
        [FunctionName("Remove")]
        public async Task<IActionResult> Remove(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string guid = req.Query["id"].ToString();
            Guid id = Guid.Parse(guid);
            await mongoDBService.Remove(id);

            return new OkObjectResult(HttpStatusCode.OK);
        }
    }
}
