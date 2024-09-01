using Domain.ReadModels;
using System.Collections.Generic;

namespace Common.Utilities
{
    public class QuestionSearchResult
    {
        public IEnumerable<QuestionReadModel> Questions { get; set; }
        public long TotalQuestions { get; set; }
    }
}
