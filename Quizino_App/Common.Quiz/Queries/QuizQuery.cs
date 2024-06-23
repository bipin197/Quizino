using Common.Quiz.Queries;
using Common.Quiz.Repositories;
using QuizModel = Domain.Quiz.Models.Quiz;

namespace Common.Queries
{
    public class QuizQuery : IQuizQuery
    {
        private readonly ICachedRepository<QuizModel> _repository;
        public QuizQuery(ICachedRepository<QuizModel> repository)
        {
            _repository = repository;  
        }

        public IList<QuizModel> GetAllActiveQuiz()
        {
            return _repository.GetItems(x => x.IsActive).ToList();
        }

        public IList<QuizModel> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
            return _repository.GetItems(x => x.Start > start && x.Finish < end).ToList();
        }

        public Task<QuizModel> GetQuiz(long key)
        {
            return Task.FromResult(_repository.GetItem(x => x.Id == key));
        }
    }
}
