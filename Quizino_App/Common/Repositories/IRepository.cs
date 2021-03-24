using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Repositories
{
    public interface IRepository<T>
    {
        T GetItem(int key);

        T GetItem(Func<T, bool> filter);

        IEnumerable<T> GetItems();
    }
}