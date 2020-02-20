using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuestion : IEntityBase
    {
        string Text { get; set; }
        AnswerOptions OptionA { get; set; }
        AnswerOptions OptionB { get; set; }
        AnswerOptions OptionC { get; set; }
        AnswerOptions OptionD { get; set; }

        IList<IQuestion> Questions { get; set; }
    }
}
