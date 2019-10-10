using JWTAuthority.API.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthority.Helpers
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly JWTSettings _settings;

        public TokenBuilder(JWTSettings settings)
        {
            _settings = settings;
        }

        public String GetToken(String username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Issuer,
              claims: GetClaims(username),
              expires: DateTime.Now.AddDays(_settings.ExpiresInDays),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] GetClaims(String username)
        {
            return new[] {
                new Claim("username", username)
            };
        }
    }
}
