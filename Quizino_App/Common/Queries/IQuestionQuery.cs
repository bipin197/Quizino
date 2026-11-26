using Common.Utilities;
using Domain.Interfaces;
using Domain.ReadModels;
using System;
using System.Collections.Generic;

namespace Common.Queries
{
    public interface IQuestionQuery : IQuery<QuestionReadModel>
    {
        QuestionReadModel GetQuestion(long id);
        QuestionReadModel GetQuestion(string hash);
        IEnumerable<QuestionReadModel> GetAllQuestions(Func<QuestionReadModel, bool> predicate);
        IEnumerable<QuestionReadModel> GetAllQuestionsByCategory(Categories category);
        IEnumerable<QuestionReadModel> GetQuestions(ReadOnlyCriteria criteria);
        long GetNextQuestionSequence();
    }
}
