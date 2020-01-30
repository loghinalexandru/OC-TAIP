using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace OcelotGateway.Models
{
    public sealed class JWTSettings
    {
        private JWTSettings()
        {
        }

        public string AuthenticationScheme { get; private set; }
        public string Key { get; private set; }

        public string Issuer { get; private set; }

        public string Audience { get; private set; }

        public string SecurityAlgorithm { get; private set; }

        public int ExpiresInDays { get; private set; }

        public TimeSpan TokenExpiration => TimeSpan.FromDays(ExpiresInDays);

        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}