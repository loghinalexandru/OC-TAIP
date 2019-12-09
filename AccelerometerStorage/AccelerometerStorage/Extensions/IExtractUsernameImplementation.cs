using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi
{
    public interface IExtractUsernameImplementation
    {
        string ExtractUsername(HttpContext context);
    }
}
