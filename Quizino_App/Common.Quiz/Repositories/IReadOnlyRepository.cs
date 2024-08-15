using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Quiz.Repositories
{
    public interface IReadOnlyRepository<T>
    {
        T GetItem(Func<T, bool> predicate);
        IEnumerable<T> GetItems(Func<T, bool> predicate);

        Task RefreshAsync();
    }
}
