using System;
using System.Collections.Generic;

namespace Common.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        T GetItem(Func<T, bool> predicate);
        IEnumerable<T> GetItems(Func<T, bool> predicate);
        IEnumerable<T> GetAllItems();
    }
}
