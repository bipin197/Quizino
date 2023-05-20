using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Common.Loaders
{
    public interface IUserLoader
    {
        IUser GetUser(int id);

        IUser GetUser(string emailId);
    }
}