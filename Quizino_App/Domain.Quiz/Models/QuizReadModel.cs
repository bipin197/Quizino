using Domain.Quiz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Quiz.Models
{
    public class QuizReadModel : EntityBase, IQuiz
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
        public QuizReadModel()
        {
            IsActive = false; // Default value
            UserCreated = string.Empty; // Default to empty string
            IsChallenge = true; // Default value
        }
    }
}
