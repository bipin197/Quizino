using Common.Commands;
using Common.Queries;
using Common.Utilities;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionApi.Store
{
    public class QuestionDataStore
    {
        private readonly IQuestionQuery _questionQuery;
        private readonly UpdateQuestionCommandHandler _updateQuestionCommandHandler;
        public QuestionDataStore(IQuestionQuery questionQuery, UpdateQuestionCommandHandler updateQuestionCommandHandler)
        {
            _questionQuery = questionQuery;
            _updateQuestionCommandHandler = updateQuestionCommandHandler;
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

        internal void AddQuestions(IEnumerable<Question> questions, CreateQuestionCommandHandler createQuestionCommand)
        {
             createQuestionCommand.HandleAsync(questions).ConfigureAwait(false);
        }

        internal async Task UpdateQuestions(IEnumerable<UpdateQuestionCommand> questions)
        {
            await _updateQuestionCommandHandler.HandleAsync(questions).ConfigureAwait(false);
        }
    }
}