using Common.Commands;
using Common.Queries;
using Common.Utilities;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuestionApi.Store
{
    public class QuestionDataStore
    {
        private readonly IQuestionQuery _questionQuery;
        public QuestionDataStore(IQuestionQuery questionQuery)
        {
            _questionQuery = questionQuery;
        }

        internal Question GetQuestion(long id)
        {
            return _questionQuery.GetQuestion(id);
        }

        internal IEnumerable<Question> GetQuestions(long[] ids)
        {
            return _questionQuery.GetAllQuestions(x => ids.Contains(x.Id));
        }

        internal IEnumerable<Question> GetQuestion(Criteria criteria)
        {
            return _questionQuery.GetQuestions(criteria);
        }

        internal void AddQuestions(IEnumerable<Question> questions, CreateQuestionCommand createQuestionCommand)
        {
             createQuestionCommand.Handle(questions).ConfigureAwait(false);
        }
    }
}