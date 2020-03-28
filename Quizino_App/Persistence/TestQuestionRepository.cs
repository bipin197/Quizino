using Domain.Interfaces;
using Persistence.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public class TestQuestionRepository
    {
        private static TestQuestionRepository _instance;
        private static IList<QuestionDto> _repository;
        public static TestQuestionRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestQuestionRepository();
                _instance.Initialize();
            }

            return _instance;
        }

        public IQuestion GetQuestion(int key)
        {
            return _repository.FirstOrDefault(z => z.Key == key);
        }

        public IEnumerable<IQuestion> GetQuestions(int numberOfQuestions)
        {
            var possible = Enumerable.Range(1, 1000).ToList();
            var listNumbers = new List<int>();
            var rand = new Random();
            for (int i = 0; i < numberOfQuestions; i++)
            {
                int index = rand.Next(0, possible.Count);
                listNumbers.Add(possible[index]);
                possible.RemoveAt(index);
            }

            foreach(var index in listNumbers)
            {
                yield return _repository[index];
            }
        }

        private void Initialize()
        {
            _repository = new List<QuestionDto>();
            for (int i = 1; i < 1000; i++)
            {
                var quiz = new QuestionDto
                {
                    Text = "Question " + i,
                    OptionA = "A " + i,
                    OptionB = "B " + i,
                    OptionC = "C " + i,
                    OptionD = "D " + i,
                    Key = i
                };

                _repository.Add(quiz);
            }
        }
    }
}
