using Common.Loaders;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class TestQuizLoader : IQuizLoader
    {
        public IQuiz GetQuiz(int id)
        {
            return new Quiz
            {
                Key = 1,
                Id = "Test Quiz"
            };
        }
    }
}
