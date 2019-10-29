using JWTAuthority.API.Models;
using JWTAuthority.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTAuthority.Helpers
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly JWTSettings _settings;
        
        public const string UsernameClaim = "username";

        public TokenBuilder(JWTSettings settings)
        {
            _settings = settings;
        }

        public string GetToken(User user)
        {
            var expirationDate = DateTime.Now.Add(_settings.TokenExpiration);
            var credentials = new SigningCredentials(_settings.SecurityKey, _settings.SecurityAlgorithm);

            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: GetClaims(user),
              expires: expirationDate,
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            return new List<Claim> 
            {
                new Claim(UsernameClaim, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };
        }
    }
}
