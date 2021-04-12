using System;
namespace Domain.Interfaces
{
    public interface IEntityBase
    {
        long Key { get; set; }

        bool IsNew { get; set; }

        bool IsValid();
    }
}
