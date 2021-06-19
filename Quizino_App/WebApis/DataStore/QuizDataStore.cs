using Domain.Interfaces;
using Persistence.Repositories;
using Persistence.DataTransferObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Utilities;
using Domain.Models;

namespace WebApis.DataStore
{
    public class QuizDataStore
    {
        private readonly CosmoDBRepository<Question> _cosmoDBQuestionRepository;
        private readonly CosmoDBRepository<Quiz> _cosmoDBQuizRepository;
        private const int NumberQuestionsForPerQuiz = 15;
        private const int HighestQuestionKey = 500;
        public QuizDataStore()
        {
            _cosmoDBQuestionRepository = CosmoDBRepository<Question>.GetInstance();
            _cosmoDBQuizRepository = CosmoDBRepository<Quiz>.GetInstance();
        }

        public async Task<IQuiz> CreateQuiz(DateTime startTime, int category)
        {
            var randomKeys = NumberHandler.GenerateRandomKeys(NumberQuestionsForPerQuiz, 1, HighestQuestionKey);
            var questions = _cosmoDBQuestionRepository.GetItems(x => randomKeys.Contains(x.Id));
            var allQuizes = _cosmoDBQuizRepository.GetItems(x => x.Id > 0);
            var nextId = 1L;
            if (allQuizes.Any())
            {
                nextId = allQuizes.Max(x => x.Id) + 1;
            }
            var quiz = new Quiz
            {
                Id = nextId,
                Key  = nextId,
                Name = Guid.NewGuid().ToString(),
                IsActive = true,
                CreationTime = startTime,
                DeactivationTime = startTime.AddMinutes(60),
                Category = Categories.All,
                QuestionKeys = string.Join(',', questions.Select(x => x.Id))
            };

            await _cosmoDBQuizRepository.AddItemAsync(quiz).ConfigureAwait(false);
            await _cosmoDBQuizRepository.SaveAsync();

            return quiz;
        }  
    }
}
