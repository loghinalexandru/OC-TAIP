using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace AccelerometerStorage.WebApi
{
    public static class Extensions
    {
        public static IExtractUsernameImplementation ExtractUsernameImplementation { get; set; } = new ExtractUsernameImplementation();

        public static string ExtractUsername(this HttpContext context) => ExtractUsernameImplementation.ExtractUsername(context);

        public static ResultResponse ToInternalResponse(this Result result)
        {
            if (result.IsFailure)
            {
                return ResultResponse.Fail(result.Error);
            }

            return ResultResponse.Ok();
        }

        public static ResultResponse<T> ToInternalResponse<T>(this Result<T> result)
        {
            if (result.IsFailure)
            {
                return (ResultResponse<T>)ResultResponse.Fail(result.Error);
            }

            return (ResultResponse<T>)ResultResponse<T>.Ok(result.Value);
        }
    }
}
