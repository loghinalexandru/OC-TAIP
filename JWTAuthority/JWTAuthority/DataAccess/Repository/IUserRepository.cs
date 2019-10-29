using JWTAuthority.Domain;
using System;

namespace JWTAuthority.DataAccess.Repository
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
        bool IsAvailableUsername(string username);
        void AddUser(User user);
    }
}
