namespace Common.Quiz.Commands
{
    public class CreateQuizCommand : ICommand
    {
        public int NumberOfQuestions { get; set; }
        public int NumberOfQuiz { get; set; }
        public bool IsActive { get; set; }
        public bool IsChallenge { get; set; }
        public bool IsUserCreated { get; set; }
        public int Category { get; set; }
        public int MaxTime { get; set; }
    }
}
