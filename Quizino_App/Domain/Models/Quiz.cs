using Domain.Interfaces;
using System;

namespace Domain.Models
{
    public class Quiz : EntityBase, IQuiz
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeactivationTime { get; set; }
        public Categories Category { get; set; }
        public long[] QuestionKeys { get; set; }
    }
}
