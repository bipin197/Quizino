using Common.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CachedQuestionRepository : ICachedRepository<Question>
    {
        private readonly IRepository<Question> _questionRepository;
        private readonly List<Question> _questions;
        public CachedQuestionRepository(IRepository<Question> questionRepository)
        {
            _questionRepository = questionRepository;
            _questions = _questionRepository.GetItems(x => true).ToList();
        }

        public async Task AddItemAsync(Question item)
        {
           await _questionRepository.AddItemAsync(item);
        }

        public async Task AddItemsAsync(IEnumerable<Question> item)
        {
            await _questionRepository.AddItemsAsync(item);
        }

        public Question GetItem(Func<Question, bool> predicate) => _questions.FirstOrDefault(predicate);
        
        public IEnumerable<Question> GetItems(Func<Question, bool> predicate) => _questions.Where(predicate);
      
        public async Task RefreshAsync()
        {
            _questions.Clear();
            _questions.AddRange(await Task.FromResult(_questionRepository.GetItems(x => true)));
        }

        public void RemoveItem(Question item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItems(IEnumerable<Question> item)
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
