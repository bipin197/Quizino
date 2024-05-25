using Domain.Quiz.Interfaces;
using QuizModel = Domain.Quiz.Models.Quiz;
namespace Common.Quiz.Queries
{
    public interface IQuizQuery : IQuery<QuizModel>
    {
        Task<QuizModel> GetQuiz(long key);

        IList<QuizModel> GetAllActiveQuiz();

        IList<QuizModel> GetAllActiveQuizForPeriod(DateTime start, DateTime end);
    }
}
