using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DataTransferObjects
{
    public class QuestionSet
    {
        public QuestionDto[] Questions;
        public long HighestKeyInSet { get; set; }
    }
}
