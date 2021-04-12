using Domain.Interfaces;

namespace Domain.Models
{
    public abstract class EntityBase : IEntityBase
    {
        public long Key { get; set; }

        public bool IsNew { get; set; }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
