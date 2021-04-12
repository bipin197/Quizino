using Domain.Interfaces;
using Persistence.DataTransferObjects;
using Persistence.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CosmoDbUserRepository : DocumentDBRepository<IUser>,  IHasGetNextKey
    {
        private static CosmoDbUserRepository _instance;
        private static List<IUser> _repository;
        public CosmoDbUserRepository()
        {
        }

        public async Task<IList<IUser>> GetUsers()
        {
            if (!_repository.Any())
            {
                var result = await _instance.GetAllItemsAsStringAsync();
                if(!result.Any())
                {
                    return new List<IUser>();
                }

                var users = new JsonParser<UserDto>().Parse(result);
                _repository.AddRange(users);
            }

            return _repository.Cast<IUser>().ToList();
        }


        public async Task<IUser> CreateUser(IUser user)
        {
            await GetUsers();
            await _instance.CreateItemAsync(user);
            _repository.Add(user);

            return user;
        }

        public long GetNextKey()
        {
            if (_repository.Any())
            {
                return _repository.Max(x => x.Key) + 1;
            }

            return 1;
        }
    }
}