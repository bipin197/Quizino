using System;

namespace Domain.Interfaces
{
    public interface IQuiz : IEntityBase
    {
        long Id { get; set; }
        string Name { get; set; }

        bool IsActive { get; set; }

        DateTime CreationTime { get; set; }

        DateTime DeactivationTime { get; set; }

        Categories Category { get; set; } 

        string QuestionKeys { get; set; }
    }
}
