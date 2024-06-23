using Common.Quiz.Repositories;
using Domain.Quiz.Models;
using Domain.Quiz.Interfaces;
using Newtonsoft.Json;
using QuizModel = Domain.Quiz.Models.Quiz;

namespace Persistent.Quiz.Repositories
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
            if (typeof(T) == typeof(QuizModel))
            {
                string path = GetFilePath();

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

                    if (typeof(T) == typeof(QuizModel))
                    {
                        var quiz = x as QuizModel;
                        if (quiz != null)
                        {
                            quiz.Id = x.Id;
                        }
                    }
                });
                _repository.AddRange(result);
            }
        }

        private static string GetFilePath()
        {
            var path = string.Empty;
            if (typeof(T) == typeof(QuizModel))
            {
                path = @"Data/QuizData.json";
            }

            return path;
        }

        private AnswerOptions GetAnswerOptions(string answerInText)
        {
            if (answerInText.Equals("A"))
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
            throw new NotSupportedException();
        }

        public Task AddItemsAsync(IEnumerable<T> item)
        {
            throw new NotSupportedException();
        }

        public T GetItem(Func<T, bool> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetItems(Func<T, bool> predicate)
        {
            return _repository.Where(predicate);
        }
        public IEnumerable<T> GetAllItems()
        {
            return _repository;
        }

        public void UpdateItem(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveItems(IEnumerable<T> item)
        {
            throw new NotSupportedException();
        }

        public async Task SaveAsync()
        {
            Save();
            await Task.FromResult(_repository).ConfigureAwait(false);
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(_repository, Formatting.Indented);

            // Write the JSON to the file
            try
            {
                File.WriteAllText(GetFilePath(), json);
                _repository.Clear();
                Initialize();
            }
            catch (Exception ex)
            {

            }
        }

        public Task UpdateItemsAsync(IEnumerable<T> items)
        {
            throw new NotSupportedException();
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
