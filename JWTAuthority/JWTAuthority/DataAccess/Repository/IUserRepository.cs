using JWTAuthority.Domain;
using System;

namespace JWTAuthority.DataAccess.Repository
{
    public interface IUserRepository
    {
        User GetByUsername(String username);
        void AddUser(User user);
    }
}
