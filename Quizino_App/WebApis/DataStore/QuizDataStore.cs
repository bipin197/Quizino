using Domain.Interfaces;
using Persistence;
using Persistence.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApis.DataStore
{
    public class QuizDataStore
    {
        private readonly CosmoDBQuestionRepository _cosmoDBQuestionRepository;
        private readonly CosmoDbQuizRepository _cosmoDBQuizRepository;
        public QuizDataStore()
        {
            _cosmoDBQuestionRepository = CosmoDBQuestionRepository.GetInstance();
            _cosmoDBQuizRepository = CosmoDbQuizRepository.GetInstance();
        }

        public async Task<IQuiz> CreateQuiz(DateTime startTime)
        {
            var randomKeys = GenerateRandomKeys();
            var questions = await _cosmoDBQuestionRepository.GetQuestions(x => randomKeys.Contains(x.Key));

            var quiz = new QuizDto
            {
                Key = _cosmoDBQuizRepository.GetNextKey(),
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                CreationTime = startTime,
                Category = Categories.All,
                QuestionKeys = questions.Select(x => x.Key).ToArray()
            };

            return await _cosmoDBQuizRepository.CreateQuiz(quiz);
        }

        //TODO: make it random
        private IEnumerable<long> GenerateRandomKeys()
        {
            for (long key = 15; key < 31; key++)
            {
                yield return key;
            }
        }
    }
}
