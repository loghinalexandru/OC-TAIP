using JWTAuthority.Domain;
using System;

namespace JWTAuthority.DataAccess.Repository
{
    public interface IUserRepository
    {
        public User GetByUsername(String username);
        public void AddUser(User user);
    }
}
