using System;

namespace Domain.Interfaces
{
    public interface IQuiz : IEntityBase
    {
        string Id { get; set; }

        bool IsActive { get; set; }

        DateTime CreationTime { get; set; }

        DateTime DeactivationTime { get; set; }

        Categories Category { get; set; } 

        long[] QuestionKeys { get; set; }
    }
}
