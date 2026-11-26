using Domain.Interfaces;
using System;
namespace Common.Services
{
    public interface IUserService
    {
        IUser GetUser();
        void StoreUser();
    }
}
