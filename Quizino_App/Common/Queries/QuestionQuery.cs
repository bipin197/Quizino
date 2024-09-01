using Common.Repositories;
using Common.Utilities;
using Domain.Interfaces;
using Domain.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Queries
{
    public class QuestionQuery : IQuestionQuery
    {
        private readonly IReadOnlyRepository<QuestionReadModel> _repository;  
        public QuestionQuery(IReadOnlyRepository<QuestionReadModel> repository)
        {
            _repository = repository;
        }

        public IEnumerable<QuestionReadModel> GetAllQuestions(Func<QuestionReadModel, bool> predicate)
        {
            return _repository.GetItems(predicate).AsEnumerable();
        }

        public IEnumerable<QuestionReadModel> GetAllQuestionsByCategory(Categories category)
        {
            return _repository.GetItems(x => x.ApplicableCategories.Contains(category.ToString()));
        }

        public long GetNextQuestionSequence()
        {
            return _repository.GetItems(x => x.Id > 0).Max(x => x.Id) + 1;   
        }

        public QuestionReadModel GetQuestion(long id)
        {
            return _repository.GetItem(x => x.Id == id);
        }

        public QuestionReadModel GetQuestion(string hash)
        {
            return _repository.GetItem(x => x.HashCode == hash);
        }

        public IEnumerable<QuestionReadModel> GetQuestions(ReadOnlyCriteria criteriaData)
        {
            if(criteriaData.Ids.Any())
            {
                return _repository.GetItems(x => criteriaData.Meets(x));
            }

            //TODO: must change this
            return _repository.GetItems(x => true);
        }
    }
}
