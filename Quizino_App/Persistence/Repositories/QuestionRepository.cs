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

        public async Task UpdateItemsAsync(IEnumerable<Question> items)
        {
            var ids = items.Select(x => x.Id);
            var questions = _questionDbContext.Questions.Where(x => ids.Contains(x.QuestionId));
            foreach(var question in questions)
            {
                var item = items.Where(x => x.Id == question.Id).FirstOrDefault();
                if(item != null)
                {
                    question.Text = item.Text;
                    question.OptionA = item.OptionA;
                    question.OptionB = item.OptionB;
                    question.OptionC = item.OptionC;
                    question.OptionD = item.OptionD;
                    question.Answer = item.Answer;
                    question.ApplicableCategories= item.ApplicableCategories??"0";
                }
            }

            foreach (var item in items.Where(x => x.IsNew))
            {
                var question = new Question
                {
                    Text = item.Text,
                    OptionA = item.OptionA,
                    OptionB = item.OptionB,
                    OptionC = item.OptionC,
                    OptionD = item.OptionD,
                    Answer = item.Answer,
                    ApplicableCategories = item.ApplicableCategories??"0"
                };

                _questionDbContext.Questions.Add(question);
            }

            await _questionDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
