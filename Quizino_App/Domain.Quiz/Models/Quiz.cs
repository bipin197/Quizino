using Domain.Quiz.Interfaces;
using System;

namespace Domain.Quiz.Models
{
    public class Quiz : EntityBase, IQuiz
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeactivationTime { get; set; }
        public Categories Category { get; set; }
        public string QuestionKeys { get; set; }
        public long QuizId { get; set; }
    }
}
