using Common.Quiz.Repositories;
using Domain.Quiz.Models;
using Persistent.Quiz.DbContexts;

namespace Persistent.Quiz.Repositories
{
    public class CachedQuizReadOnlyRepository : IReadOnlyRepository<QuizReadModel>
    {
        private readonly List<QuizReadModel> _quizes;
        private readonly QuizReadModelDbContext _quizReadModelDbContext;
        public CachedQuizReadOnlyRepository(QuizReadModelDbContext quizReadModeDbContext)
        {
            _quizReadModelDbContext = quizReadModeDbContext;
            _quizes = _quizReadModelDbContext.Quizes.Where(x => true).ToList();
        }

        public QuizReadModel GetItem(Func<QuizReadModel, bool> predicate) => _quizes.FirstOrDefault(x => predicate(x));

        public IEnumerable<QuizReadModel> GetItems(Func<QuizReadModel, bool> predicate)
        {
            return _quizes.Where(x => predicate(x));
        }

        public async Task RefreshAsync()
        {
            _quizes.Clear();
            _quizes.AddRange(await Task.FromResult(_quizReadModelDbContext.Quizes.Where(x => true)));
        }
    }
}
