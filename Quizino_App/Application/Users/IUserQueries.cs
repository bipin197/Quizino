using Domain.Interfaces;

namespace Application.Users
{
    public interface IUserQueries
    {
        IUser GetUser(int key);

        IUser GetUser(string emailId);

        void StoreUser(IUserQueries user);
    }
}