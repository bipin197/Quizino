using AppLogic;
using Common.Commands;
using Common.Queries;
using Common.Utilities;
using Domain.Models;
using Domain.ReadModels;
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

        internal IEnumerable<long> GetRandomActiveQuestionKeys(int numberOfQuestions)
        {
            var allQuestionKeys = _questionQuery.GetAllQuestions(x => x.QuestionId > 0).Select(x => x.QuestionId);
            return QuestionRandomizer.GetRandomKeysFromAvailableKeys(allQuestionKeys, numberOfQuestions);
        }

        internal IEnumerable<string> GetRandomActiveQuestionHashes(int numberOfQuestions)
        {
            var allQuestions = _questionQuery.GetAllQuestions(x => x.QuestionId > 0);
            var allQuestionKeys = allQuestions.Select(x => x.QuestionId);
            var keys = QuestionRandomizer.GetRandomKeysFromAvailableKeys(allQuestionKeys, numberOfQuestions);

            foreach(var key in keys)
            {
                yield return allQuestions.FirstOrDefault(x => x.QuestionId == key).HashCode;
            }     
        }

        internal QuestionReadModel GetQuestion(long id)
        {
            return _questionQuery.GetQuestion(id);
        }

        internal QuestionReadModel GetQuestionFromHash(string hash)
        {
            return _questionQuery.GetQuestion(hash);
        }

        internal IEnumerable<QuestionReadModel> GetQuestions(long[] ids)
        {
            return _questionQuery.GetAllQuestions(x => ids.Contains(x.QuestionId));
        }

        internal IEnumerable<QuestionReadModel> GetQuestion(ReadOnlyCriteria criteria)
        {
            return _questionQuery.GetQuestions(criteria);
        }

        internal async Task AddQuestions(IEnumerable<Question> questions, CreateQuestionCommandHandler createQuestionCommand)
        {
             await createQuestionCommand.HandleAsync(questions).ConfigureAwait(false);
        }

        internal async Task UpdateQuestions(IEnumerable<UpdateQuestionCommand> questions)
        {
            await _updateQuestionCommandHandler.HandleAsync(questions).ConfigureAwait(false);
        }
    }
}