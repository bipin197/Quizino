using Common.Repositories;
using Domain.ReadModels;
using Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.Repositories
{
    public class ReadOnlyQuestionRepository : IReadOnlyRepository<QuestionReadModel>
    {
        private readonly QuestionReadModelDbContext _questionDbContext;
        private readonly IEnumerable<QuestionReadModel> _questions;
        public ReadOnlyQuestionRepository(QuestionReadModelDbContext dbContext)
        {
            _questionDbContext = dbContext;
            _questions = _questionDbContext.Questions;
        }

        public IEnumerable<QuestionReadModel> GetAllItems()
        {
            return _questions;
        }

        public QuestionReadModel GetItem(Func<QuestionReadModel, bool> predicate)
        {
            return _questions.FirstOrDefault(x => predicate(x));
        }

        public IEnumerable<QuestionReadModel> GetItems(Func<QuestionReadModel, bool> predicate)
        {
            var questions = _questions.Where(x => predicate(x));
            try
            {
                if(questions.Any())
                {
                    return questions;
                }

                return Enumerable.Empty<QuestionReadModel>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Enumerable.Empty<QuestionReadModel>();
            }
        }
    }
}
