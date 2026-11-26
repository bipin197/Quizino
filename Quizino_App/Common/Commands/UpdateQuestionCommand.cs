using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Commands
{
    public class UpdateQuestionCommand : ICommand
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string ApplicableCategories { get; set; }
        public int Answer { get; set; }
        public bool IsNew { get; set; }

    }
}
