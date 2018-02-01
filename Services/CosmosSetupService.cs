using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Options;
using RealEstateCatalog.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RealEstateCatalog.Services
{
    public interface ISetupService
    {
        Task GenerateDataAsync();
    }

    public class CosmosSetupService : ISetupService
    {
        private CosmosSettings CosmosSettings { get; }
        private IHostingEnvironment Environment { get; }

        public CosmosSetupService(IOptions<CosmosSettings> cosmosSettings, IHostingEnvironment environment)
        {
            CosmosSettings = cosmosSettings.Value;         
            Environment = environment;   
        }

        public async Task GenerateDataAsync()
        {
            await Task.FromResult(default(object));
        }

        private IEnumerable<Home> GetHomes()
        {
            string jsonDocument = File.ReadAllText(Path.Combine(Environment.ContentRootPath, "data.json"));
            return JsonConvert.DeserializeObject<IList<Home>>(jsonDocument);
        }
    }
}