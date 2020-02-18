using Domain.Interfaces;

namespace Domain.Models
{
    public class Question : EntityBase, IQuestion
    {
        public string Text { get; set; }
        public AnswerOptions OptionA { get; set; }
        public AnswerOptions OptionB { get; set; }
        public AnswerOptions OptionC { get; set; }
        public AnswerOptions OptionD { get; set; }
    }
}
