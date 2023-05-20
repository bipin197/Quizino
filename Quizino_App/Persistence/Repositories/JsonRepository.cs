using Common.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly List<T> _repository;

        public JsonRepository()
        {
            _repository = new List<T>();
            Initialize();
        }
    
        private void Initialize()
        {
            if(typeof(T) == typeof(Question) || typeof(T) == typeof(Quiz))
            {
                var path = string.Empty;
                if (typeof(T) == typeof(Question))
                {
                    path = @"Data/QuestionData.json";
                }
                if (typeof(T) == typeof(Quiz))
                {
                    path = @"Data/QuizData.json";
                }

                var content = File.ReadAllText(path);
                if (string.IsNullOrEmpty(content))
                {
                    return;
                }

                var result = JsonConvert.DeserializeObject<T[]>(content).ToList();
                var key = 1;
                result.ForEach(x =>
                {
                    x.Id = key++;
                    if (typeof(T) == typeof(Question))
                    {
                        var question = x as Question;
                        if (question != null)
                        {
                            question.QuestionId = x.Id;
                            question.ApplicableCategories = "0";
                        }
                    }

                    if (typeof(T) == typeof(Quiz))
                    {
                        var quiz = x as Quiz;
                        if (quiz != null)
                        {
                            quiz.QuizId = x.Id;
                        }
                    }
                });
                _repository.AddRange(result);
            }
        }

        private AnswerOptions GetAnswerOptions(string answerInText)
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
        public IEnumerable<T> GetAllItems()
        {
            return _repository;
        }

        public void UpdateItem(T item)
        {
            throw new System.NotImplementedException();
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

        public void Save()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateItemsAsync(IEnumerable<T> items)
        {
            var ids = items.Select(x => x.Id);
            foreach (var id in ids)
            {
                var re = _repository.Where(x => x.Id == id).FirstOrDefault();
                var item = items.FirstOrDefault(x => x.Id == id);
            }

            await Task.FromResult(_repository).ConfigureAwait(false);
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
