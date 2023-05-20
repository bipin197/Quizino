using Common.Repositories;
using Domain.Models;
using Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly QuestionDbContext _questionDbContext;
        public QuestionRepository(QuestionDbContext dbContext)
        {
            _questionDbContext = dbContext;
        }

        public async Task AddItemAsync(Question item) 
        { 
            await Task.FromResult(_questionDbContext.Questions.Add(item)).ConfigureAwait(false);
        }
        
        public async Task AddItemsAsync(IEnumerable<Question> items)
        {
             _questionDbContext.Questions.AddRange(items);
        }

        public Question GetItem(Func<Question, bool> predicate)
        {
            return _questionDbContext.Questions.FirstOrDefault(predicate);
        }

        public IEnumerable<Question> GetItems(Func<Question, bool> predicate)
        {
            return _questionDbContext.Questions.Where(predicate);
        }

        public void RemoveItem(Question item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItems(IEnumerable<Question> item)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync() => await _questionDbContext.SaveChangesAsync().ConfigureAwait(false);

        public void Save() => _questionDbContext.SaveChanges();

        public void UpdateItem(Question item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemsAsync(IEnumerable<Question> item)
        {
            throw new NotImplementedException();
        }
    }
}
