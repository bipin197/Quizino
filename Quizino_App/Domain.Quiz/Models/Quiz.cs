using Domain.Quiz.Interfaces;

namespace Domain.Quiz.Models
{
    public class Quiz : EntityBase, IQuiz
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public Categories Category { get; set; }
        public string QuesHash { get; set; }
        public int MaxTime { get; set; }
        public bool IsActive { get; set; }
        public string UserCreated { get; set; }
        public bool IsChallenge { get; set; }

        // Constructor
        public Quiz()
        {
            IsActive = false; // Default value
            UserCreated = string.Empty; // Default to empty string
            IsChallenge = true; // Default value
        }
    }
}
