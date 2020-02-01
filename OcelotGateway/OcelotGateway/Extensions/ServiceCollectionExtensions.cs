using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OcelotGateway.Models;
using System.Text;

namespace OcelotGateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration settings)
        {
            services.AddAuthentication()
                .AddJwtBearer(settings[nameof(JwtSettings.AuthenticationScheme)], options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings[nameof(JwtSettings.Issuer)],
                        ValidAudience = settings[nameof(JwtSettings.Audience)],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings[nameof(JwtSettings.Key)])),
                    };
                });

            return services;
        }
    }
}