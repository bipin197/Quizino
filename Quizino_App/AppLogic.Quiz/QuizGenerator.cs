using Domain.Quiz.Interfaces;
using QuizModel = Domain.Quiz.Models.Quiz;

namespace AppLogic.Quiz
{
    public static class QuizGenerator
    {
        public static IQuiz GenerateQuiz(int numberOfQuestions)
        {
            var quiz = new QuizModel
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
