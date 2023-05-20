using Common.Utilities;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Common.Queries
{
    public interface IQuestionQuery : IQuery<Question>
    {
        Question GetQuestion(long id);
        IList<Question> GetAllQuestions(Func<Question, bool> predicate);
        IList<Question> GetAllQuestionsByCategory(Categories category);
        IList<Question> GetQuestions(Criteria criteria);
        long GetNextQuestionSequence();
    }
}
