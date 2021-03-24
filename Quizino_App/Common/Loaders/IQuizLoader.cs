using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Loaders
{
    public interface IQuizLoader
    {
        Task<IQuiz> GetQuiz(int key);

        IList<IQuiz> GetAllActiveQuiz();

        IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end);

        IQuestion GetQuestion(long quizKey, int questionNumber);
    }
}
