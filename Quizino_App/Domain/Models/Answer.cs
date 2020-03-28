using Domain.Interfaces;

namespace Domain.Models
{
    public class Answer : EntityBase, IAnswer
    {
        public long QuestionKey { get; set; }
        public AnswerOptions CorrectAnswer { get; set; }
    }
}
