using Domain.Interfaces;
using Persistence.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistence
{
    public class TestQuizRepository
    {
        private static TestQuizRepository _instance;
        private static IList<IQuiz> _repository;
        public static TestQuizRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new TestQuizRepository();
                _instance.Initialize();
            }

            return _instance;
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
            for(int i = 0; i < 100; i++)
            {
                var quiz = new QuizDto
                {
                    Key = i + 1,
                    Id = "Test Quiz " + i + 1,
                    IsActive = i > 90,
                    CreationTime = DateTime.Now.AddMinutes(i),
                    DeactivationTime = DateTime.Now.AddMinutes(60 + i)
                };

                _repository.Add(quiz);
            }
        }
    }
}
