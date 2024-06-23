using Domain.Models;
using System;
using System.Linq;

namespace Common.Utilities
{
    public class Criteria
    {
        public long[] Ids { get; set; }
        public string[] HashCodes { get; set; }
        public string Categories { get; set; }

        public bool Meets(Question question)
        {
            return Ids.Contains(question.Id) || HashCodes.Contains(question.HashCode);
        }
    }
}
