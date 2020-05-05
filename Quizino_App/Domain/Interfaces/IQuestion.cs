using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IQuestion : IEntityBase
    {
        string Text { get; set; }
        string OptionA { get; set; }
        string OptionB { get; set; }
        string OptionC { get; set; }
        string OptionD { get; set; }
        IEnumerable<Categories> ApplicableCategories { get; set; }
        AnswerOptions Answer { get; set; }
    }
}
