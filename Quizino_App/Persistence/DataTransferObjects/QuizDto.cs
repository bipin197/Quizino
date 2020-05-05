using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DataTransferObjects
{
    public class QuizDto : EntityBase, IQuiz
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeactivationTime { get; set; }
        public Categories Category { get; set; }
    }
}
