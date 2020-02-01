using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AccelerometerStorage.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi
{
    public sealed class ExtractEmailImplementation : IExtractEmailImplementation
    {
        public string ExtractEmail(HttpContext context)
        {
            var headerToken = context.Request.Headers["Authorization"].FirstOrDefault()
                ?.Replace("Bearer", string.Empty)
                .Trim();

            return
                new JwtSecurityToken(headerToken)?
                    .Claims
                    .FirstOrDefault(claim => claim.Type == CustomClaims.EmailClaim)?
                    .Value;
        }
    }
}