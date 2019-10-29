using FluentValidation.AspNetCore;
using JWTAuthority.API.Extensions;
using JWTAuthority.DataAccess;
using JWTAuthority.DataAccess.Repository;
using JWTAuthority.Helpers;
using JWTAuthority.Service;
using JWTAuthority.Service.Exceptions;
using JWTAuthority.Service.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;

namespace JWTAuthority
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<JWTContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthorityDatabase"))
            );

            services.AddControllers()
                .AddFluentValidation(
                    fv => fv.RegisterValidatorsFromAssemblyContaining<AuthenticationServiceValidator>());

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Authority", Version = "v1"}); });

            AddDependencyInjection(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseRouting();
            app.UseCors();


            app.UseMiddleware<ExceptionMiddleware>();

            app.UpdateDatabase();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authority v1"); });
        }

        private void AddDependencyInjection(IServiceCollection services)
        {
            services.AddInfrastructure();

            services.AddSingleton<HashAlgorithm>(new SHA256CryptoServiceProvider());

            services.AddTransient<IValidatorInterceptor, ValidationInterceptor>();

            services.AddScoped<IHashHelper, HashHelper>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}