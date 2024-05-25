using Domain.Quiz.Interfaces;
using Domain.Quiz.Models;

namespace Persistent.Quiz.DataTransferObjects
{
    public class QuizDto : EntityBase, IQuiz
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeactivationTime { get; set; }
        public Categories Category { get; set; }
        public string QuestionKeys { get; set; }
    }
}
