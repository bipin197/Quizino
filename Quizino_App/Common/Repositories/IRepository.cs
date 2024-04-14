using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public interface IRepository<T>
    {
        Task AddItemAsync(T item);
        Task AddItemsAsync(IEnumerable<T> items);

        T GetItem(Func<T, bool> predicate);
        IEnumerable<T> GetItems(Func<T, bool> predicate);

        void UpdateItem(T item);
        Task UpdateItemsAsync(IEnumerable<T> items);

        void RemoveItem(T item);
        void RemoveItems(IEnumerable<T> item);

        Task SaveAsync();

        void Save();
    }
}
