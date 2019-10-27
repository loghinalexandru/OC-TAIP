using AccelerometerStorage.Business;
using AccelerometerStorage.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AccelerometerStorage.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddSettings<JWTSettings>();
            services.AddSettings<StorageSettings>();

            return services;
        }
    }
}
