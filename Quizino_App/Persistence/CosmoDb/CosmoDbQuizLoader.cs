using Common.Loaders;
using Domain.Interfaces;
using Domain.Models;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.CosmoDb
{
    public class CosmoDbQuizLoader : IQuizLoader
    {
        public IList<IQuiz> GetAllActiveQuiz()
        {
            return CosmoDBRepository<Quiz>.GetInstance().GetItems(x => x.IsActive).Cast<IQuiz>().ToList();
        }

        public IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
            return CosmoDBRepository<Quiz>.GetInstance().GetItems(x => x.CreationTime >= start && x.DeactivationTime <= end).Cast<IQuiz>().ToList(); ;
        }

        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            return CosmoDBRepository<Question>.GetInstance().GetItem(x => x.Id == questionNumber);
        }

        public async Task<IQuiz> GetQuiz(int key)
        {
            return CosmoDBRepository<Quiz>.GetInstance().GetItem(x => x.Id == key);
        }
    }
}
