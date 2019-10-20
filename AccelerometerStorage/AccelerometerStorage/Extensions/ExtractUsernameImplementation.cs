using System.Linq;
using AccelerometerStorage.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi.Extensions
{
    public sealed class ExtractUsernameImplementation : IExtractUsernameImplementation
    {
        public string ExtractUsername(HttpContext context) => context.User.Claims.First(claim => claim.Type == CustomClaims.UsernameClaim).Value;
    }
}
