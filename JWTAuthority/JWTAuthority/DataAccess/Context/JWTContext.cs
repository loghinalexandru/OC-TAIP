using JWTAuthority.Domain;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthority.DataAccess
{
    public class JWTContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public JWTContext(DbContextOptions options) : base(options)
        {

        }
    }
}
