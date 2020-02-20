using Common.Loaders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
            return _quizLoader.GetQuiz(key);
        }
    }
}
