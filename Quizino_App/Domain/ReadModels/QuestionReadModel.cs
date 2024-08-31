using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.ReadModels
{
    public class QuestionReadModel : EntityBase, IQuestion
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long QuestionId { get => Id; set => Id = value; }

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

        public string HashCode { get; set; }
    }
}
