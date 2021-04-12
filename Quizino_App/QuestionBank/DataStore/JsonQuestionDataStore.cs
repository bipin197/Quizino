using Domain.Interfaces;
using Domain.Models;
using Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionBank.DataStore
{
    public class JsonQuestionDataStore : IDataStore
    {
        public IEnumerable<IQuestion> LoadQuestions()
        {
            var questionRepos = JsonQuestionRepository.GetInstance();
            return questionRepos.GetQuestions();
        }

        public async Task SaveQuestions(IEnumerable<IQuestion> questions)
        {
            var questionRepos = JsonQuestionRepository.GetInstance();
            await questionRepos.SaveData(questions.ToArray()).ConfigureAwait(false);
        }
    }
}
