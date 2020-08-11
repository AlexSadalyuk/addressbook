using Core.Models;
using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserShortInfo> GetUsers(string domain);
        UserDetails GetUser(int id, string domain);
        UserDetails UpdateUser(UserDetails userUpdate, string domain);
        UserDetails AddUser(UserDetails userUpdate, string domain);
        void DeleteUser(int id, string domain);
    }
}
