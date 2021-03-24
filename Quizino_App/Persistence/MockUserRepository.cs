using Common.Repositories;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public class MockUserRepository : IRepository<IUser>
    {
        private static MockUserRepository _instance;
        private static IList<IUser> _repository;
        public static MockUserRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new MockUserRepository();
                _instance.Initialize();
            }

            return _instance;
        }

        public IUser GetItem(int key)
        {
            return _repository.FirstOrDefault(x => x.Key ==key);
        }

        public IUser GetItem(Func<IUser, bool> filter)
        {
            return _repository.FirstOrDefault(filter);
        }

        public IEnumerable<IUser> GetItems()
        {
            return _repository;
        }

        private void Initialize()
        {
            _repository = new List<IUser>();
            var userRepository = TestQuizRepository.GetInstance();
            for (int i = 1; i < 101; i++)
            {
                _repository.Add(new User 
                {
                    Key = i,
                    EmailId = "User" + i + "@testMail.com",
                    UserName = "John Dow " + i,
                    Location = "Hyderabad",
                    AccessType = "Email"
                });
            }
        }
    }
}