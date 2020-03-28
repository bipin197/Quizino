using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAnswer
    {
        long QuestionKey { get; set; }
        AnswerOptions CorrectAnswer { get; set; }
    }
}
