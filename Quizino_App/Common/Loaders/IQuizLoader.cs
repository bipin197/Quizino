using Domain.Interfaces;

namespace Common.Loaders
{
    public interface IQuizLoader
    {
        IQuiz GetQuiz(int id);
    }
}
