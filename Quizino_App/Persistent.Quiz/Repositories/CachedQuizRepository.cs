using Common.Quiz.Repositories;
using Persistent.Quiz.DbContexts;
using QuizModel = Domain.Quiz.Models.QuizWriteModel;

namespace Persistent.Quiz.Repositories
{
    public class CachedQuizRepository : IRepository<QuizModel>
    {
        private readonly QuizWriteModelDbContext _quizWriteModelDbContext;
        private readonly List<QuizModel> _quizes;
        public CachedQuizRepository(QuizWriteModelDbContext quizWriteModelDbContext)
        {
            _quizWriteModelDbContext = quizWriteModelDbContext;
            _quizes = _quizWriteModelDbContext.Quizes.Where(x => true).ToList();
        }
        public async Task AddItemAsync(QuizModel item)
        {
            await _quizWriteModelDbContext.Quizes.AddAsync(item);
        }

        public async Task AddItemsAsync(IEnumerable<QuizModel> item)
        {
            await _quizWriteModelDbContext.Quizes.AddRangeAsync(item);
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
            _quizes.AddRange(await Task.FromResult(_quizWriteModelDbContext.Quizes.Where(x => true)));
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
