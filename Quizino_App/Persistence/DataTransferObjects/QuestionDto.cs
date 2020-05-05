using Domain.Interfaces;
using Domain.Models;
using System.Collections.Generic;

namespace Persistence.DataTransferObjects
{
    public class QuestionDto : EntityBase, IQuestion
    {
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public IEnumerable<Categories> ApplicableCategories { get; set; }
        public AnswerOptions Answer { get; set; }
    }
}
