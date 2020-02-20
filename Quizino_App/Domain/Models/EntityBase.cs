using Domain.Interfaces;

namespace Domain.Models
{
    public abstract class EntityBase : IEntityBase
    {
        public long Key { get; set; }
    }
}
