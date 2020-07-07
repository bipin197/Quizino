using Common.Loaders;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Persistence
{
    public class TestQuizLoader : IQuizLoader
    {
        public IList<IQuiz> GetAllActiveQuiz()
        {
            return TestQuizRepository.GetInstance().GetAllActiveQuiz();
        }

        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            return TestQuizRepository.GetInstance().GetQuestion(quizKey, questionNumber);
        }

        public IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
            return TestQuizRepository.GetInstance().GetAllActiveQuizForAPeriod(start, end);
        }

        public IQuiz GetQuiz(int key)
        {
           return TestQuizRepository.GetInstance().GetQuiz(key);
        }
    }
}
