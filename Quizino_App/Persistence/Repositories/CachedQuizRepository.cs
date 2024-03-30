using Common.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CachedQuizRepository : ICachedRepository<Quiz>
    {
        public Task AddItemAsync(Quiz item)
        {
            throw new NotImplementedException();
        }

        public Task AddItemsAsync(IEnumerable<Quiz> item)
        {
            throw new NotImplementedException();
        }

        public Quiz GetItem(Func<Quiz, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quiz> GetItems(Func<Quiz, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(Quiz item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItems(IEnumerable<Quiz> item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Quiz item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemsAsync(IEnumerable<Quiz> item)
        {
            throw new NotImplementedException();
        }
    }
}
