using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace JWTAuthority.API.Models
{
    public sealed class JWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set;  }
        public string Audience { get; set; }
        public string SecurityAlgorithm { get; set; }
        public int ExpiresInDays { get; set; }
        public TimeSpan TokenExpiration => TimeSpan.FromDays(ExpiresInDays);
        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
