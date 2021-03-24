using Domain.Interfaces;
using Persistence.DataTransferObjects;
using Persistence.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CosmoDBQuestionRepository : DocumentDBRepository<QuestionSet>, IHasGetNextKey
    {
        private static CosmoDBQuestionRepository _instance;
        private static List<QuestionDto> _repository;

        public static CosmoDBQuestionRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CosmoDBQuestionRepository();
                _instance.Initialize("CollectionId");
            }

            return _instance;
        }

        protected override void Initialize(string collectionId)
        {        
            _repository = new List<QuestionDto>();
            base.Initialize(collectionId);
        }

        public async Task<IList<IQuestion>> GetQuestions(Func<IQuestion, bool> predicate)
        {
            if(!_repository.Any())
            {
                var result = await _instance.GetAllItemsAsStringAsync();
                var questionSets = JsonParser<QuestionSet>.Parse(result);
                _repository.AddRange(questionSets.Where(x => x.Questions != null).SelectMany(x => x.Questions));
            }

            return _repository.Cast<IQuestion>().Where(predicate).ToList();
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

            _repository.AddRange(questionDtos);
            await _instance.CreateItemAsync(questionSet);
        }

        public long GetNextKey()
        {
            return _repository.Max(x => x.Key) + 1;
        }
    }
}
