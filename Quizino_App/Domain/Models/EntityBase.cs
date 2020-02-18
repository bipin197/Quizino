using Domain.Interfaces;

namespace Domain.Models
{
    public class EntityBase : IEntityBase
    {
        public long Key { get; set; }
    }
}
