using AccelerometerStorage.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccelerometerStorage.Persistance.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StorageContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped<IReadRepository<DataFile>, DataFileReadRepository>();

            return services;
        }
    }
}
