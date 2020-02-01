using System;
using System.Collections.Generic;
using System.Text;
using AccelerometerStorage.Infrastructure;
using CSharpFunctionalExtensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace AccelerometerStorage.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Storage API",
                });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddGeneralConfiguration(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            return services;
        }

        public static IServiceCollection BuildMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ModelValidationFilter));
                    options.EnableEndpointRouting = false;
                })
                .AddFluentValidation(fvc => { fvc.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions();

            return services;
        }

        private static IMvcBuilder AddJsonOptions(this IMvcBuilder builder)
        {
            builder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new GuidJsonConverter());
            });

            return builder;
        }

        private class GuidJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                Result.SuccessIf(!EqualityComparer<Guid>.Default.Equals((Guid) value, default(Guid)), value, "error")
                    .Match(
                        writer.WriteValue,
                        _ => writer.WriteNull());
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var token = JToken.Load(reader);
                object res = Guid.Empty;

                Result.SuccessIf(token != null, token, "error")
                    .Ensure(t => t.Type != JTokenType.Null, "error")
                    .Ensure(t => Guid.TryParse(t.ToString(), out _), "error")
                    .OnSuccessTry(t => res = t.ToObject(objectType));

                return res;
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Guid);
            }
        }

    }
}
