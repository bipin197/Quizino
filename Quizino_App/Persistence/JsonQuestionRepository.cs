using Domain.Interfaces;
using Newtonsoft.Json;
using Ninject.Injection;
using Persistence.DataTransferObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Persistence
{
    public class JsonQuestionRepository
    {
        private static JsonQuestionRepository _instance;
        private static List<QuestionDto> _repository;
        public static JsonQuestionRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new JsonQuestionRepository();
                Initialize();
            }

            return _instance;
        }

        public IList<IQuestion> GetQuestions()
        {
            return _repository.Cast<IQuestion>().ToList();
        }

        public void SaveData(IQuestion[] questions)
        {
            var content = JsonConvert.SerializeObject(questions);
            File.Delete(@"Data\questions.json");
            var stream = File.Create(@"Data\questions.json");
            stream.Close();
            File.WriteAllText(@"Data\questions.json", content);
        }

        private static void Initialize()
        {
            _repository = new List<QuestionDto>();
            var content = File.ReadAllText(@"Data\questions.json");
            if(string.IsNullOrEmpty(content))
            {
                return;
            }

            var questions = JsonConvert.DeserializeObject<QuestionDto[]>(content);
            _repository.AddRange(questions);
        }
    }
}
