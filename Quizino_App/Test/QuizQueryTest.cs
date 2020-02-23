using Application.Quiz;
using System;
using System.Linq;
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

        [Fact]
        public void TestQuizQueryWithNoLoader()
        {
            var quizQuery = new QuizQueries(null);
            var quiz = quizQuery.GetQuiz(1);

            Assert.True(quiz == null);
        }

        [Fact]
        public void TestActiceQuizQuery()
        {
            var quizQuery = new QuizQueries(new TestQuizLoader());
            var quiz = quizQuery.GetAllActiveQuiz();

            Assert.True(quiz.All(x => x.IsActive));
        }

        [Fact]
        public void TestActiceQuizQueryForAPeriod()
        {
            var quizQuery = new QuizQueries(new TestQuizLoader());
            var quiz = quizQuery.GetAllActiveQuizForPeriod(DateTime.Now, DateTime.Now.AddMinutes(60));

            Assert.True(quiz.Count == 9);
        }
    }
}
