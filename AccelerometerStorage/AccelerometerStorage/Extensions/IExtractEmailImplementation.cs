using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi
{
    public interface IExtractEmailImplementation
    {
        string ExtractEmail(HttpContext context);
    }
}