using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Question : EntityBase, IQuestion
    {
        public string Text { get; set; }
        public AnswerOptions OptionA { get; set; }
        public AnswerOptions OptionB { get; set; }
        public AnswerOptions OptionC { get; set; }
        public AnswerOptions OptionD { get; set; }
        public IList<IQuestion> Questions { get; set; }
    }
}
