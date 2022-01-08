using Domain.Interfaces;
using Domain.Models;
using Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionApi.Store
{
    public class QuestionDataStore
    {
        private readonly CosmoDBRepository<Question> _cosmoDBQuestionRepository;
        public QuestionDataStore()
        {
            _cosmoDBQuestionRepository = CosmoDBRepository<Question>.GetInstance();
        }

        internal Question GetQuestion(long id)
        {
            return _cosmoDBQuestionRepository.GetItem(x => x.Id == id);
        }

        internal IEnumerable<Question> GetQuestions(long[] ids)
        {
            return _cosmoDBQuestionRepository.GetItems(x => ids.Contains(x.Id));
        }

        internal async Task<bool> ProcessQuestions(IEnumerable<Question> questions)
        {
            await EnsureMandatoryDefaultProperties(questions);
            if (!CanProcess(questions))
            {
                return false;
            }

            await _cosmoDBQuestionRepository.AddItemsAsync(questions.Cast<Question>());
            await SaveToDb().ConfigureAwait(false);
            return true;
        }

        private async Task EnsureMandatoryDefaultProperties(IEnumerable<Question> questions)
        {
            await _cosmoDBQuestionRepository.EnsureCreatedAsync();
            var allItems = _cosmoDBQuestionRepository.GetItems(x => x.Id > 0);
            var maxId = 0L;
            if (allItems.Any())
            {
                maxId = allItems.Max(x => x.Id);
            }
            foreach (var question in questions)
            {
                if (question.Id <= 0)
                {
                    question.Id = maxId + 1;
                    maxId++;
                }
                if (string.IsNullOrEmpty(question.ApplicableCategories))
                {
                    question.ApplicableCategories = ((int)Categories.GeneralAwareness).ToString();
                }
            }
        }

        internal async Task SaveToDb()
        {
            await _cosmoDBQuestionRepository.SaveAsync();
        }

        private bool CanProcess(IEnumerable<Question> questions)
        {
            foreach (var question in questions)
            {
                if (!question.IsValid())
                {
                    return false;
                }
            }

            return true;
        }
    }
}