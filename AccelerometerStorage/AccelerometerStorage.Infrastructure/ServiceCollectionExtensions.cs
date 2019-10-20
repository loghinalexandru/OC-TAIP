using AccelerometerStorage.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AccelerometerStorage.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddSettings<JWTSettings>();
        }
    }
}
