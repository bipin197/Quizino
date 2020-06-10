using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Persistence
{
    public class DocumentDBRepository<T> where T : class
    {
        private static DocumentClient client;
        protected DbConnectionBundle DbConnectionBundle { get; set; }

        public DocumentDBRepository()
        {
 
        }

        internal async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        internal async Task<IEnumerable<string>> GetAllItemsAsStringAsync()
        {
            var documents = client.CreateDocumentQuery<dynamic>(UriFactory.CreateDocumentCollectionUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId),
            "SELECT * FROM c").AsEnumerable().Select(x => x);

            var results = new List<string>();
            foreach (var document in documents)
            {
                var content = document.ToString();
                results.Add(content);
            }

            return results;
        }

        internal async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task DeleteDatabase()
        {
            await client.DeleteDatabaseAsync(DbConnectionBundle.DatabaseId);
        }

        internal async Task<Document> CreateItemAsync(T item)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId), item);
        }

        internal async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId, id), item);
        }

        internal async Task DeleteItemAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId, id));
        }

        protected virtual void Initialize()
        {
            client = new DocumentClient(new Uri(DbConnectionBundle.EndpointConnection.Endpoint), DbConnectionBundle.EndpointConnection.Key);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DbConnectionBundle.DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DbConnectionBundle.DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DbConnectionBundle.DatabaseId, DbConnectionBundle.CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DbConnectionBundle.DatabaseId),
                        new DocumentCollection { Id = DbConnectionBundle.CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
