using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuizQuestion
    {
        long QuestionKey { get; set; }
        long QuizKey { get; set; }
    }
}
