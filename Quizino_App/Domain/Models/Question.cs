using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Question : EntityBase, IQuestion
    {
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
    }
}
