using AccelerometerStorage.Business;
using AccelerometerStorage.Common;
using AccelerometerStorage.Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace AccelerometerStorage.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddSingleton<IQueueHelper, QueueHelper>();
            services.AddSettings<StorageSettings>();
            services.AddSettings<QueueSettings>();

            return services;
        }
    }
}
