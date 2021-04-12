using Microsoft.Extensions.Configuration;
using Persistence.CosmoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CosmoDBQuestionRepository1<T> : IRepository<T> where T : class
    {
        private readonly CosmoDbContext<T> _dbContext;
        private static CosmoDBQuestionRepository1<T> _instance;

        public static CosmoDBQuestionRepository1<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CosmoDBQuestionRepository1<T>(GetDbConnectionBundle());
            }

            return _instance;
        }
        public CosmoDBQuestionRepository1(DbConnectionBundle dbConnectionBundle)
        {
            _dbContext = new CosmoDbContext<T>(dbConnectionBundle);          
        }

        public async Task AddItemAsync(T item)
        {
            await _dbContext.Items.AddAsync(item);
        }

        public async Task EnsureDeletedAsync()
        {
            await _dbContext.Database.EnsureDeletedAsync();
        }

        public async Task EnsureCreatedAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();
        }

        public async Task AddItemsAsync(IEnumerable<T> items)
        {
            await _dbContext.Items.AddRangeAsync(items);
        }

        public T GetItem(Func<T, bool> predicate)
        {
            return _dbContext.Items.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetItems(Func<T, bool> predicate)
        {
            return _dbContext.Items.Where(predicate);
        }

        public void RemoveItem(T item)
        {
            _dbContext.Items.Remove(item);
        }

        public void RemoveItems(IEnumerable<T> items)
        {
            _dbContext.Items.RemoveRange(items);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateItem(T item)
        {
            _dbContext.Items.Update(item);
        }

        public void UpdateItems(IEnumerable<T> items)
        {
            _dbContext.Items.UpdateRange(items);
        }

        private static DbConnectionBundle GetDbConnectionBundle()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var dbConnectionBundle = new DbConnectionBundle
            {
                EndpointConnection = new EndpointConnection
                {
                    Endpoint = config["EndPoint"],
                    Key = config["Key"]
                },
                DatabaseId = config["DatabaseId"],
                //TODO: Take from settings file
                CollectionId = config["MyCollection"]
            };

            return dbConnectionBundle;
        }
    }
}
