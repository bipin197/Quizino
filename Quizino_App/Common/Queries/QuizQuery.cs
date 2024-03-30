using Common.Loaders;
using Common.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Queries
{
    public class QuizQuery : IQuizQuery
    {
        private readonly ICachedRepository<Quiz> _repository;
        public QuizQuery(ICachedRepository<Quiz> repository)
        {
            _repository = repository;  
        }

        public IList<Quiz> GetAllActiveQuiz()
        {
            return _repository.GetItems(x => x.IsActive).ToList();
        }

        public IList<Quiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
            return _repository.GetItems(x => x.CreationTime > start && x.DeactivationTime < end).ToList();
        }

        public Task<Quiz> GetQuiz(long key)
        {
            return Task.FromResult(_repository.GetItem(x => x.Id == key));
        }
    }
}
