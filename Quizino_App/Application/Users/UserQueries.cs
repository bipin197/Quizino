using Common.Loaders;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Users
{
    public class UserQueries
    {
        private readonly IUserLoader _userLoader;
        public UserQueries(IUserLoader loader)
        {
            _userLoader = loader;
        }

        public IUser GetUser(int key)
        {
            return _userLoader.GetUser(key);  
        }

        public IUser GetUser(string emailId)
        {
             return _userLoader.GetUser(emailId);  
        }

        public void StoreUser(IUserQueries user)
        {

        }
    }
}