using JWTAuthority.API.Models;
using JWTAuthority.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthority.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services.AddTransient<ITokenBuilder, TokenBuilder>()
                .AddSettings<JWTSettings>();
        }
    }
}
