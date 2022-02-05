using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T :  EntityBase
    {
        private static JsonRepository<T> _instance;
        private static List<T> _repository;

        public static JsonRepository<T> GetInstance()
        {
            if (_instance == null)
            {
                _instance = new JsonRepository<T>();
                _repository = new List<T>();
                Initialize();
            }

            return _instance;
        }

        private static void Initialize()
        {
            if (typeof(T) == typeof(Question))
            {
                var content = File.ReadAllText(@"Data/QuestionData.json");
                if (string.IsNullOrEmpty(content))
                {
                    return;
                }

                var result = JsonConvert.DeserializeObject<T[]>(content).ToList();
                var key = 1;
                result.ForEach(x => 
                {
                    x.Key = key++;
                });
                _repository.AddRange(result);
            }
        }

        private static AnswerOptions GetAnswerOptions(string answerInText)
        {
            if(answerInText.Equals("A"))
            {
                return AnswerOptions.A;
            }
            if (answerInText.Equals("B"))
            {
                return AnswerOptions.B;
            }
            if (answerInText.Equals("C"))
            {
                return AnswerOptions.C;
            }
            if (answerInText.Equals("D"))
            {
                return AnswerOptions.D;
            }

            return AnswerOptions.A;
        }

        public Task AddItemAsync(T item)
        {
            throw new System.NotSupportedException();
        }

        public Task AddItemsAsync(IEnumerable<T> item)
        {
            throw new System.NotSupportedException();
        }

        public T GetItem(System.Func<T, bool> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetItems(System.Func<T, bool> predicate)
        {
            return _repository.Where(predicate);
        }

        public void UpdateItem(T item)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateItems(IEnumerable<T> item)
        {
            throw new System.NotSupportedException();
        }

        public void RemoveItem(T item)
        {
            throw new System.NotSupportedException();
        }

        public void RemoveItems(IEnumerable<T> item)
        {
            throw new System.NotSupportedException();
        }

        public Task SaveAsync()
        {
            throw new System.NotSupportedException();
        }

        private class QuestionTransferObject
        {
            public string Question { get; set; }

            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }

            public string Answer { get; set; }
        }
    }
}
