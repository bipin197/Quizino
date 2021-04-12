using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuestionBank.DataStore
{
    public interface IDataStore
    {
        IEnumerable<IQuestion> LoadQuestions();
        Task SaveQuestions(IEnumerable<IQuestion> questions);
    }
}
