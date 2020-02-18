using Domain.Interfaces;

namespace Domain.Models
{
    public class Quiz : EntityBase, IQuiz
    {
        public string Id { get; set; }
    }
}
