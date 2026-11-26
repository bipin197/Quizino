using Domain.Interfaces;
using Persistence.DataTransferObjects;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class TestQuestionRepository : IRepository<IQuestion>
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

        public Task AddItemAsync(IQuestion item)
        {
            throw new NotSupportedException();
        }

        public Task AddItemsAsync(IEnumerable<IQuestion> item)
        {
            throw new NotSupportedException();
        }

        public IQuestion GetItem(int id)
        {
            return _repository.FirstOrDefault(z => z.Id == id);
        }

        public IQuestion GetItem(Func<IQuestion, bool> filter)
        {
            return _repository.FirstOrDefault(filter);
        }

        public IEnumerable<IQuestion> GetItems() => _repository;

        public IEnumerable<IQuestion> GetItems(Func<IQuestion, bool> predicate)
        {
            return _repository.Where(predicate);
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

        public void RemoveItem(IQuestion item)
        {
            throw new NotSupportedException();
        }

        public void RemoveItems(IEnumerable<IQuestion> item)
        {
            throw new NotSupportedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotSupportedException();
        }

        public void UpdateItem(IQuestion item)
        {
            throw new NotSupportedException();
        }

        public Task UpdateItemsAsync(IEnumerable<IQuestion> item)
        {
            throw new NotImplementedException();
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
                    Id = i
                };

                _repository.Add(quiz);
            }
        }
    }
}
