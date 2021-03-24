using Common.Loaders;
using Domain.Interfaces;
using System;

namespace Persistence
{
    public class TestUserLoader : IUserLoader
    {
        public IUser GetUser(int id)
        {
            return MockUserRepository.GetInstance().GetItem(id);
        }

        public IUser GetUser(string emailId)
        {
            return MockUserRepository.GetInstance()
            .GetItem(x => string.Equals(x.EmailId, emailId, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
