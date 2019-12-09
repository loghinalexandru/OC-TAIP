using AccelerometerStorage.Persistance.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccelerometerStorage.WebApi
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<StorageContext>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}
