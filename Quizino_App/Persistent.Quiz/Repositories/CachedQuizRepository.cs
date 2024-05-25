using Common.Quiz.Repositories;
using QuizModel = Domain.Quiz.Models.Quiz;

namespace Persistent.Quiz.Repositories
{
    public class CachedQuizRepository : ICachedRepository<QuizModel>
    {
        private readonly IRepository<QuizModel> _quizRepository;
        private readonly List<QuizModel> _quizes;
        public CachedQuizRepository(IRepository<QuizModel> quizRepository)
        {
            _quizRepository = quizRepository;
            _quizes = _quizRepository.GetItems(x => true).ToList();
        }
        public async Task AddItemAsync(QuizModel item)
        {
            await _quizRepository.AddItemAsync(item);
        }

        public async Task AddItemsAsync(IEnumerable<QuizModel> item)
        {
            await _quizRepository.AddItemsAsync(item);
        }

        public QuizModel GetItem(Func<QuizModel, bool> predicate)
        {
            return _quizes.FirstOrDefault(x => predicate(x));
        }

        public IEnumerable<QuizModel> GetItems(Func<QuizModel, bool> predicate)
        {
            return _quizes.Where(x => predicate(x));
        }

        public async Task RefreshAsync()
        {
            _quizes.Clear();
            _quizes.AddRange(await Task.FromResult(_quizRepository.GetItems(x => true)));
        }

        public void RemoveItem(QuizModel item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItems(IEnumerable<QuizModel> item)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(QuizModel item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemsAsync(IEnumerable<QuizModel> item)
        {
            throw new NotImplementedException();
        }
    }
}
