using Common.Loaders;
using Domain.Interfaces;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.CosmoDb
{
    public class CosmoDbQuizLoader : IQuizLoader
    {
        public IList<IQuiz> GetAllActiveQuiz()
        {
            throw new NotSupportedException();
        }

        public IList<IQuiz> GetAllActiveQuizForPeriod(DateTime start, DateTime end)
        {
            throw new NotSupportedException();
        }

        public IQuestion GetQuestion(long quizKey, int questionNumber)
        {
            throw new NotSupportedException();
        }

        public async Task<IQuiz> GetQuiz(int key)
        {
            return await CosmoDbQuizRepository.GetInstance().GetQuiz(key);
        }
    }
}
