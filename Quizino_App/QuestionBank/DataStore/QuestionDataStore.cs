using Domain.Interfaces;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuestionBank.DataStore
{
    public class QuestionDataStore
    {
        public IList<IQuestion> LoadQuestions()
        {
            var questionRepos = JsonQuestionRepository.GetInstance();

            return questionRepos.GetQuestions();
        }

        public void SaveQuestions(IList<IQuestion> questions)
        {
            var questionRepos = JsonQuestionRepository.GetInstance();

            questionRepos.SaveData(questions.ToArray());
        }
    }
}
