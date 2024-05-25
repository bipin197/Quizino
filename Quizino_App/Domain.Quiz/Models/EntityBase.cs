using Domain.Quiz.Interfaces;

namespace Domain.Quiz.Models
{
    public abstract class EntityBase : IEntityBase
    {
        public long Id { get; set; }

        public bool IsNew { get; set; }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
