using JetBrains.Annotations;
using JWTAuthority.Domain;
using System.Linq;

namespace JWTAuthority.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly JWTContext _context;

        public UserRepository(JWTContext context)
        {
            _context = context;
        }

        public bool IsAvailableUsername(string username)
        {
            return
                _context.Users.Find(username) == null;
        }

        public void AddUser(User user)
        {
            _context.Add(user);

            _context.SaveChanges();
        }

        [NotNull]
        public User GetByUsername(string username)
        {
            return
                _context.Users.SingleOrDefault(user => user.Username == username);
        }
    }
}