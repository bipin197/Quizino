using Domain.Quiz.Interfaces;

namespace Persistent.Quiz.DataTransferObjects
{
    public class QuizDto
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public required string QuesHash { get; set; }
        public int MaxScore { get; set; }
        public int MaxTime { get; set; }
        public required string UserCreated { get; set; }
        public bool IsChallenge { get; set; }
        public Categories Category { get; set;}
        public bool IsActive { get ; set; }
    }
}
