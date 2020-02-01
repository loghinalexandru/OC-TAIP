﻿using AccelerometerStorage.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AccelerometerStorage.WebApi
{
    public sealed class ExtractUsernameImplementation : IExtractUsernameImplementation
    {
        public string ExtractUsername(HttpContext context)
        {
            var headerToken = context.Request.Headers["Authorization"].FirstOrDefault()
                ?.Replace("Bearer", string.Empty)
                .Trim();

            return
                new JwtSecurityToken(headerToken)?
                    .Claims
                    .FirstOrDefault(claim => claim.Type == CustomClaims.UsernameClaim)?
                    .Value;
        }
    }
}