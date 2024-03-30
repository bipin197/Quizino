using Common.Loaders;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace QuestionApi.Store
{
    public class QuizDataStore
    {
        private readonly IQuizQuery _quizQuery;
        public QuizDataStore(IQuizQuery quizQuery)
        {
            _quizQuery = quizQuery; 
        }

        internal IEnumerable<Quiz> GetAllActiveQuiz()
        {
            throw new NotImplementedException();
        }

        internal Quiz GetQuiz(long id)
        {
            throw new NotImplementedException();
        }
    }
}
