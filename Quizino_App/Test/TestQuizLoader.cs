using Common.Loaders;
using Domain.Interfaces;
using Domain.Models;
using Persistence;
using System;
using System.Collections.Generic;

namespace Test
{
    public class TestQuizLoader : IQuizLoader
    {
        public IList<IQuiz> GetAllActiveQuiz()
        {
            return TestQuizRepository.GetInstance().GetAllActiveQuiz();
        }

        public IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
           return TestQuizRepository.GetInstance().GetAllActiveQuizForAPeriod(start, end);
        }

        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            throw new NotImplementedException();
        }

        public IQuiz GetQuiz(int key)
        {
            return TestQuizRepository.GetInstance().GetQuiz(key);
        }
    }
}
