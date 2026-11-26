using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Commands
{
    public interface ICommandHandler<T>
    {
        Task HandleAsync(IEnumerable<T> items);
    }
}
