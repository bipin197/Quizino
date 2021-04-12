using Domain.Interfaces;
using Newtonsoft.Json;
using Persistence.DataTransferObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class JsonQuestionRepository
    {
        private static JsonQuestionRepository _instance;
        private static List<QuestionDto> _repository;
        private static BlobHandler _blobHandler;
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

        public async Task SaveData(IQuestion[] questions)
        {
            var options = Formatting.Indented;
            var content = JsonConvert.SerializeObject(questions, options);
            await _blobHandler.SaveData(content).ConfigureAwait(false);
        }

        private static void Initialize()
        {
            _repository = new List<QuestionDto>();
            var content = File.ReadAllText(@"\\INHYDL4106\Data\Question-Data.json");
            if(string.IsNullOrEmpty(content))
            {
                return;
            }

            if(_blobHandler == null)
            {
                _blobHandler = new BlobHandler();
            }

            var questions = JsonConvert.DeserializeObject<QuestionTransferObject[]>(content);
            _repository.AddRange(TransformObjectsFromJsonFile(questions));
        }

        private static IEnumerable<QuestionDto> TransformObjectsFromJsonFile(QuestionTransferObject[] questionTransferObjects)
        {
            foreach(var item in questionTransferObjects)
            {
                yield return new QuestionDto()
                {
                    Text = item.Question,
                    OptionA = item.A,
                    OptionB = item.B,
                    OptionC = item.C,
                    OptionD = item.D,
                    ApplicableCategories = ((int)Categories.GeneralAwareness).ToString(),
                    Answer = (int)GetAnswerOptions(item.Answer)
                };
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
