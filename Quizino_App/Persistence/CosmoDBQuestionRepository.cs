using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Persistence.DataTransferObjects;
using Persistence.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class CosmoDBQuestionRepository : DocumentDBRepository<QuestionSet>
    {
        private static CosmoDBQuestionRepository _instance;
        private static List<QuestionDto> _repository;

        public static CosmoDBQuestionRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CosmoDBQuestionRepository();
                _instance.Initialize();
            }

            return _instance;
        }

        protected override void Initialize()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DbConnectionBundle = new DbConnectionBundle
            {
                EndpointConnection = new EndpointConnection
                {
                    Endpoint = config["EndPoint"],
                    Key = config["Key"]
                },
                DatabaseId = config["DatabaseId"],
                CollectionId = config["CollectionId"]
            };

            _repository = new List<QuestionDto>();
            base.Initialize();
        }

        public async Task<IList<IQuestion>> GetQuestions()
        {
            if(!_repository.Any())
            {
                var result = await _instance.GetAllItemsAsStringAsync();
                var questionSets = JsonParser<QuestionSet>.Parse(result);
                _repository.AddRange(questionSets.Where(x => x.Questions != null).SelectMany(x => x.Questions));
            }

            return _repository.Cast<IQuestion>().ToList();
        }

        public async Task CreateQuestionAsync(QuestionSet question)
        {
            await _instance.CreateItemAsync(question);
        }

        public async Task SaveData(IQuestion[] questions)
        {
            var questionDtos = new List<QuestionDto>();
            foreach(var quesion in questions)
            {
                var dto = new QuestionDto
                {
                    Key = quesion.Key,
                    Text = quesion.Text,
                    OptionA = quesion.OptionA,
                    OptionB = quesion.OptionB,
                    OptionC = quesion.OptionC,
                    OptionD = quesion.OptionD,
                    Answer = quesion.Answer,
                    ApplicableCategories = quesion.ApplicableCategories
                };

                questionDtos.Add(dto);
            }

            var questionSet = new QuestionSet
            {
                HighestKeyInSet = questions.Max(x => x.Key),
                Questions = questionDtos.ToArray()
            };

            await _instance.CreateItemAsync(questionSet);
        }
    }
}
