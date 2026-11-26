using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Persistence.DataTransferObjects
{
    public class QuestionDto : EntityBase, IQuestion
    {
        public long Id { get; set; }

        [JsonProperty("question")]
        public string Text { get; set; }

        [JsonProperty("A")]
        public string OptionA { get; set; }

        [JsonProperty("B")]
        public string OptionB { get; set; }

        [JsonProperty("C")]
        public string OptionC { get; set; }

        [JsonProperty("D")]
        public string OptionD { get; set; }
        public string ApplicableCategories { get; set; }
        public int Answer { get; set; }
    }
}
