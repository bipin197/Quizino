using Common.Loaders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Quiz
{
    public interface IQuizQueries
    {
        IQuiz GetQuiz(int id);
    }
}
