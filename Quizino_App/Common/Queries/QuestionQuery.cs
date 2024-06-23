using Common.Repositories;
using Common.Utilities;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Queries
{
    public class QuestionQuery : IQuestionQuery
    {
        private readonly ICachedRepository<Question> _repository;  
        public QuestionQuery(ICachedRepository<Question> repository)
        {
            _repository = repository;
        }

        public IList<Question> GetAllQuestions(Func<Question, bool> predicate)
        {
            return _repository.GetItems(predicate).ToList();
        }

        public IList<Question> GetAllQuestionsByCategory(Categories category)
        {
            return _repository.GetItems(x => x.ApplicableCategories.Contains(category.ToString())).ToList();
        }

        public long GetNextQuestionSequence()
        {
            return _repository.GetItems(x => x.Id > 0).Max(x => x.Id) + 1;   
        }

        public Question GetQuestion(long id)
        {
            return _repository.GetItem(x => x.Id == id);
        }

        public Question GetQuestion(string hash)
        {
            return _repository.GetItem(x => x.HashCode == hash);
        }

        public IList<Question> GetQuestions(Criteria criteriaData)
        {
            if(criteriaData.Ids.Any())
            {
                return _repository.GetItems(x => criteriaData.Meets(x)).ToList();
            }

            //TODO: must change this
            return _repository.GetItems(x => true).ToList();
        }
    }
}
