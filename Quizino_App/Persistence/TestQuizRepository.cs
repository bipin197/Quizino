using Domain.Interfaces;
using Persistence.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public class TestQuizRepository
    {
        private static TestQuizRepository _instance;
        private static IList<IQuiz> _repository;
        private Dictionary<long, IEnumerable<IQuestion>> _cachedQuestions;
        public static TestQuizRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new TestQuizRepository();
                _instance.Initialize();
            }

            return _instance;
        }

        internal IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            return _cachedQuestions[quizKey].ElementAt(questionNumber);
        }

        public IQuiz GetQuiz(int key)
        {
            return _repository.FirstOrDefault(z => z.Key == key);
        }

        public IList<IQuiz> GetAllActiveQuiz()
        {
            return _repository.Where(z => z.IsActive).ToList();
        }

        public IList<IQuiz> GetAllActiveQuizForAPeriod(DateTime start, DateTime end)
        {
            return _repository.Where(z => z.IsActive && z.CreationTime >= start && z.DeactivationTime <= end ).ToList();
        }

        private void Initialize()
        {
            _repository = new List<IQuiz>();
            var questionRepository = TestQuestionRepository.GetInstance();
            _cachedQuestions = new Dictionary<long, IEnumerable<IQuestion>>();
            for (int i = 0; i < 100; i++)
            {
                var quiz = new QuizDto
                {
                    Name = "test quiz " + i,
                    IsActive = i > 90,
                    CreationTime = DateTime.Now.AddMinutes(i),
                    DeactivationTime = DateTime.Now.AddMinutes(60 + i)
                };

                var questions = questionRepository.GetQuestions(25);
                _repository.Add(quiz);
                _cachedQuestions.Add(quiz.Key, questions);
            }
        }
    }
}
