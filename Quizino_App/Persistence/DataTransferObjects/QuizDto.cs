using Domain.Interfaces;
using Domain.Models;
using System;

namespace Persistence.DataTransferObjects
{
    public class QuizDto : EntityBase, IQuiz
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeactivationTime { get; set; }
        public Categories Category { get; set; }
        public long[] QuestionKeys { get; set; }
    }
}
