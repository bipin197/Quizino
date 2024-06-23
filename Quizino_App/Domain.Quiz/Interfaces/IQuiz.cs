
namespace Domain.Quiz.Interfaces
{
    public interface IQuiz : IEntityBase
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public Categories Category { get; set; }
        public string QuesHash { get; set; }
        public int MaxTime { get; set; }
        public bool IsActive { get; set; }
        public string UserCreated { get; set; }
        public bool IsChallenge { get; set; }
    }
}
