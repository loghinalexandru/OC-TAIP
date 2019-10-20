using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi.Extensions
{
    public interface IExtractUsernameImplementation
    {
        string ExtractUsername(HttpContext context);
    }
}
