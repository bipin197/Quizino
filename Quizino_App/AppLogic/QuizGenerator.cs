using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace AppLogic
{
    public static class QuizGenerator
    {
        public static IQuiz GenerateQuiz(int numberOfQuestions)
        {
            var quiz = new Quiz
            {
                Category = Categories.All,
                CreationTime = DateTime.Now,
                DeactivationTime = DateTime.Now.AddHours(1),
                IsActive = true,
                Id = 1
            };

            return quiz;
        }
    }
}
