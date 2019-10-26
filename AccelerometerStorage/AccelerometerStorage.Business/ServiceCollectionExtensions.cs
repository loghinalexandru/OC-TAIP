using Microsoft.Extensions.DependencyInjection;

namespace AccelerometerStorage.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
