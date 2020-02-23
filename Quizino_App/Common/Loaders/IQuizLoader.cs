using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Loaders
{
    public interface IQuizLoader
    {
        IQuiz GetQuiz(int id);

        IList<IQuiz> GetAllActiveQuiz();

        IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end);
    }
}
