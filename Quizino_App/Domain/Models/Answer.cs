using Domain.Interfaces;

namespace Domain.Models
{
    public class Answer : EntityBase, IAnswer
    {
        public IQuestion Question { get; set; }
        public AnswerOptions CorrectAnswer { get; set; }
    }
}
