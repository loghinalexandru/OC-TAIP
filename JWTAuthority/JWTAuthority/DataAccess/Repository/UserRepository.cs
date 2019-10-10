using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthority.Domain;

namespace JWTAuthority.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly JWTContext _context;

        public UserRepository(JWTContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);

            _context.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            return 
                _context.Users.SingleOrDefault(user => user.Username == username);
        }
    }
}
