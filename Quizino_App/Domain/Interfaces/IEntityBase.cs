using System;
namespace Domain.Interfaces
{
    public interface IEntityBase
    {
        long Id { get; set; }

        bool IsNew { get; set; }

        bool IsValid();
    }
}
