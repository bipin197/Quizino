using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAnswer : IEntityBase
    {
        long QuestionKey { get; set; }
        AnswerOptions CorrectAnswer { get; set; }
    }
}
