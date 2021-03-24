using Common.Loaders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Quiz
{
    public interface IQuizQueries
    {
        Task<IQuiz> GetQuiz(int id);
    }
}
