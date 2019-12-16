using JWTAuthority.API.Models;
using JWTAuthority.Domain;
using JWTAuthority.Helpers;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace JWTAuthority.Tests
{
    [TestFixture]
    class TokenBuilderTests
    {
        private TokenBuilder _tokenBuilder;

        [SetUp]
        public void Setup()
        {
            _tokenBuilder = new TokenBuilder(GetApplicationSettings());
        }

        [Test]
        public void GetToken_WhenCalled_CorrectTokenIsReturned()
        {
            //Arrange
            var testUser = GetTestUser();

            //Act
            var jwtToken = new JwtSecurityToken(_tokenBuilder.GetToken(testUser));

            //Assert
            Assert.IsTrue(jwtToken.Subject == testUser.Email);
            Assert.IsTrue(jwtToken.Claims.Any(claim => claim.Value == testUser.Username));
        }

        private JWTSettings GetApplicationSettings()
        {
            return new JWTSettings
            {
                Key = "test123456767898765432",
                Issuer = "test",
                Audience = "test",
                SecurityAlgorithm = "HS256",
                ExpiresInDays = 365
            };
        }

        private User GetTestUser()
        {
            return new User
            {
                Username = "test",
                Password = "test",
                Email = "test@gmail.com"
            };
        }
    }
}
