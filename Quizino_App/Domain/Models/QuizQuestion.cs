using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class QuizQuestion : IQuizQuestion
    {
        public long QuestionKey { get; set; }
        public long QuizKey { get; set; }
    }
}
