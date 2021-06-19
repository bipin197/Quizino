using Domain.Interfaces;
using Domain.Models;
using Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionBank.DataStore
{
    public class CosmoDBQuestionDataStore : IDataStore
    {
        public IEnumerable<IQuestion> LoadQuestions()
        {
            var questionRepos = CosmoDBRepository<Question>.GetInstance();

            return questionRepos.GetItems(x => x != null);
        }

        public async Task SaveQuestions(IEnumerable<IQuestion> questions)
        {
            var questionRepos = CosmoDBRepository<Question>.GetInstance();
            await questionRepos.AddItemsAsync(questions.Cast<Question>()).ConfigureAwait(false);
            await questionRepos.SaveAsync().ConfigureAwait(false);
        }
    }
}
