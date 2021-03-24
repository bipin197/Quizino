using Domain.Interfaces;
using Persistence.Repositories;
using Persistence.DataTransferObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Utilities;

namespace WebApis.DataStore
{
    public class QuizDataStore
    {
        private readonly CosmoDBQuestionRepository _cosmoDBQuestionRepository;
        private readonly CosmoDbQuizRepository _cosmoDBQuizRepository;
        private const int NumberQuestionsForPerQuiz = 15;
        private const int HighestQuestionKey = 47;
        public QuizDataStore()
        {
            _cosmoDBQuestionRepository = CosmoDBQuestionRepository.GetInstance();
            _cosmoDBQuizRepository = CosmoDbQuizRepository.GetInstance();
        }

        public async Task<IQuiz> CreateQuiz(DateTime startTime, int category)
        {
            var randomKeys = NumberHandler.GenerateRandomKeys(NumberQuestionsForPerQuiz, 1, HighestQuestionKey);
            var questions = await _cosmoDBQuestionRepository.GetQuestions(x => randomKeys.Contains(x.Key));

            var quiz = new QuizDto
            {
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                CreationTime = startTime,
                Category = Categories.All,
                QuestionKeys = questions.Select(x => x.Key).ToArray()
            };

            return await _cosmoDBQuizRepository.CreateQuiz(quiz);
        }  
    }
}
