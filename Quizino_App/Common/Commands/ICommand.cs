using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Commands
{
    public interface ICommand<T>
    {
        Task Handle(IEnumerable<T> items);
    }
}
