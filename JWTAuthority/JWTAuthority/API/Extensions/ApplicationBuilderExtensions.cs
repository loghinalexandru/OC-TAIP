using JWTAuthority.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JWTAuthority.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<JWTContext>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}
