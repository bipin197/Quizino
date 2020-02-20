using Application.Quiz;
using System;
using Xunit;

namespace Test
{
    public class QuizQueryTest
    {
        [Fact]
        public void TestQuizQuery()
        {
            var quizQuery = new QuizQueries(new TestQuizLoader());
            var quiz = quizQuery.GetQuiz(1);

            Assert.True(quiz.Key == 1);
        }
    }
}
