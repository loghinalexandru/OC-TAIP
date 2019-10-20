using AccelerometerStorage.Business;
using AccelerometerStorage.Infrastructure;
using AccelerometerStorage.Persistance.EntityFramework;
using AccelerometerStorage.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccelerometerStorage.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors()
                .AddPersistence(Configuration.GetConnectionString("StorageDatabase"))
                .AddBusiness()
                .AddInfrastructure()
                .AddAuthentication(Configuration.GetSection(nameof(JWTSettings)))
                .AddSwagger()
                .AddGeneralConfiguration()
                .BuildMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseAuthentication()
                .UseAuthorization()
                .UseRouting()
                .UseCors("AllowAll")
                .UseEndpoints(endpoints => { endpoints.MapControllers(); })
                .UpdateDatabase()
                .UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Storage"))
                .UseMvc();
        }
    }
}
