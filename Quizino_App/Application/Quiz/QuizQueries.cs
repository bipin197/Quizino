using Common.Loaders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Quiz
{
    public class QuizQueries : IQuizQueries
    {
        private readonly IQuizLoader _quizLoader;
        public QuizQueries(IQuizLoader loader)
        {
            _quizLoader = loader;
        }

        public IQuiz GetQuiz(int key)
        {
            return _quizLoader?.GetQuiz(key);
        }

        public IList<IQuiz> GetAllActiveQuiz()
        {
            return _quizLoader?.GetAllActiveQuiz();
        }

        public IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
            return _quizLoader?.GetAllActiveQuiz();
        }
    }
}
