using Common.Loaders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Quiz
{
    public class QuizQueries : IQuizQueries
    {
        private readonly IQuizLoader _quizLoader;
        public QuizQueries(IQuizLoader loader)
        {
            _quizLoader = loader;
        }

        public Task<IQuiz> GetQuiz(int key)
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

        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            return _quizLoader?.GetQuestion(quizKey, questionNumber);
        }
    }
}
