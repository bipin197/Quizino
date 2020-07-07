using Domain.Interfaces;
using Persistence.DataTransferObjects;
using Persistence.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class CosmoDbQuizRepository : DocumentDBRepository<IQuiz>, IHasGetNextKey
    {
        private static CosmoDbQuizRepository _instance;
        private static List<IQuiz> _repository;

        public static CosmoDbQuizRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CosmoDbQuizRepository();
                _repository = new List<IQuiz>();
                _instance.Initialize("QuizCollectionId");
            }

            return _instance;
        }

        public async Task<IList<IQuiz>> GetQuizes()
        {
            if (!_repository.Any())
            {
                var result = await _instance.GetAllItemsAsStringAsync();
                var quizzes = JsonParser<QuizDto>.Parse(result);
                _repository.AddRange(quizzes);
            }

            return _repository.Cast<IQuiz>().ToList();
        }

        public async Task<IQuiz> CreateQuiz(IQuiz quiz)
        {
            await GetQuizes();
            await _instance.CreateItemAsync(quiz);
            _repository.Add(quiz);

            return quiz;
        }

        public long GetNextKey()
        {
            if (_repository.Any())
            {
                return _repository.Max(x => x.Key) + 1;
            }

            return 1;
        }
    }
}
