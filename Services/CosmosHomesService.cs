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
            DocumentClient client = new DocumentClient(new Uri(CosmosSettings.EndpointUrl), CosmosSettings.AuthorizationKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = CosmosSettings.DatabaseId });
            DocumentCollection collection = await client.CreateDocumentCollectionIfNotExistsAsync(database.SelfLink, new DocumentCollection { Id = CosmosSettings.ContainerId }, new RequestOptions { OfferThroughput = 400 });
            
            List<Home> homes = new List<Home>();

            string continuationToken = String.Empty;            
            do
            {
                FeedResponse<dynamic> feedResponse = await client.ReadDocumentFeedAsync(collection.DocumentsLink, new FeedOptions { MaxItemCount = 10, RequestContinuation = continuationToken });
                foreach (Home home in feedResponse)
                {
                    homes.Add(home);
                }
                continuationToken = feedResponse.ResponseContinuation;
            }
            while (!String.IsNullOrEmpty(continuationToken));
            
            return homes;
        }

        public async Task<IList<Home>> GetHomesForRealtorAsync(string source)
        {            
            DocumentClient client = new DocumentClient(new Uri(CosmosSettings.EndpointUrl), CosmosSettings.AuthorizationKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = CosmosSettings.DatabaseId });
            DocumentCollection collection = await client.CreateDocumentCollectionIfNotExistsAsync(database.SelfLink, new DocumentCollection { Id = CosmosSettings.ContainerId }, new RequestOptions { OfferThroughput = 400 });
            
            var documentQuery = client.CreateDocumentQuery<Home>(collection.SelfLink)
                .Where(home => home.Realtor == source)
                .AsDocumentQuery<Home>();

            List<Home> homes = new List<Home>();
            while (documentQuery.HasMoreResults)
            {
                homes.AddRange(await documentQuery.ExecuteNextAsync<Home>());
            }
            return homes;
        }

        public async Task<Home> GetHomeAsync(string documentId)
        {            
            DocumentClient client = new DocumentClient(new Uri(CosmosSettings.EndpointUrl), CosmosSettings.AuthorizationKey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = CosmosSettings.DatabaseId });
            DocumentCollection collection = await client.CreateDocumentCollectionIfNotExistsAsync(database.SelfLink, new DocumentCollection { Id = CosmosSettings.ContainerId }, new RequestOptions { OfferThroughput = 400 });
            
            return await client.ReadDocumentAsync<Home>(
                UriFactory.CreateDocumentUri(CosmosSettings.DatabaseId, CosmosSettings.ContainerId, documentId)
            );
        }
    }
}