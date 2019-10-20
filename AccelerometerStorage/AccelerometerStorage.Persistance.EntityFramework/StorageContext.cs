using System;
using System.Linq;
using AccelerometerStorage.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    public sealed class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; private set; }

        public DbSet<DataFile> DataFiles { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            typeof(StorageContext)
                .Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsConstructedGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .Select(Activator.CreateInstance)
                .ToList()
                .ForEach(configuration => modelBuilder.ApplyConfiguration((dynamic)configuration));
        }
    }

    
}
