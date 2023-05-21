using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public class QuestionSearchResult
    {
        public IEnumerable<Question> Questions { get; set; }
        public long TotalQuestions { get; set; }
    }
}
