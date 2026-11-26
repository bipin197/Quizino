using Common.Quiz.Queries;
using Domain.Quiz.Models;
using System;
using System.Collections.Generic;

namespace QuizApi.Store
{
    public class QuizDataStore
    {
        private readonly IQuizQuery _quizQuery;
        public QuizDataStore(IQuizQuery quizQuery)
        {
            _quizQuery = quizQuery;
        }

        internal IEnumerable<QuizWriteModel> GetAllActiveQuiz()
        {
            throw new NotImplementedException();
        }

        internal QuizWriteModel GetQuiz(long id)
        {
            throw new NotImplementedException();
        }
    }
}
