using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Options;
using RealEstateCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCatalog.Services
{
    public interface IHomesService
    {
        Task<IList<Home>> GetHomesAsync();

        Task<IList<Home>> GetHomesForRealtorAsync(string source);

        Task<Home> GetHomeAsync(string documentId);
    }
    
    public sealed class CosmosHomesService : IHomesService
    {
        private CosmosSettings CosmosSettings { get; }

        public CosmosHomesService(IOptions<CosmosSettings> cosmosSettings)
        {
            CosmosSettings = cosmosSettings.Value;            
        }

        public async Task<IList<Home>> GetHomesAsync()
        {
            return await Task.FromResult(default(IList<Home>));
        }

        public async Task<IList<Home>> GetHomesForRealtorAsync(string source)
        {
            return await Task.FromResult(default(IList<Home>));
        }

        public async Task<Home> GetHomeAsync(string documentId)
        {
            return await Task.FromResult(default(Home));
        }
    }
}