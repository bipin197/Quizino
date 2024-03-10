using System.Threading.Tasks;

namespace Common.Repositories
{
    public interface ICachedRepository<T> : IRepository<T>
    {
        Task RefreshAsync();
    }
}
