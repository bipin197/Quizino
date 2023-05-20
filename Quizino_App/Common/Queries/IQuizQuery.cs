using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Loaders
{
    public interface IQuizQuery : IQuery<Quiz>
    {
        Task<Quiz> GetQuiz(long key);

        IList<Quiz> GetAllActiveQuiz();

        IList<Quiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end);
    }
}
