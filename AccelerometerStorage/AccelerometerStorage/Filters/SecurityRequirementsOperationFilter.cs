using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AccelerometerStorage.WebApi
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requiredScopes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy)
                .Distinct();
            var enumerable = requiredScopes as string[] ?? requiredScopes.ToArray();
            if (enumerable.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

                var oAuthScheme = new OpenApiSecurityScheme
                    { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" } };
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement { [oAuthScheme] = enumerable.ToList() },
                };
            }
        }
    }
}
