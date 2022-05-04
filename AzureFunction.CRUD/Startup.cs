using AzureFunction.CRUD;
using AzureFunction.CRUD.Infrastructure;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AzureFunction.CRUD
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton((s) =>
            {
                MongoDBService mongoDBService = new MongoDBService();

                return mongoDBService;
            });
        }
    }
}
