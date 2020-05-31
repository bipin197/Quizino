using Domain.Interfaces;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.DataStore
{
    public class QuestionDataStore
    {
        public IList<IQuestion> LoadQuestions()
        {
            var questionRepos = CosmoDBQuestionRepository.GetInstance();

            return questionRepos.GetQuestions().Result;
        }

        public async Task SaveQuestions(IList<IQuestion> questions)
        {
            var questionRepos = CosmoDBQuestionRepository.GetInstance();

            await questionRepos.SaveData(questions.ToArray());
        }
    }
}
