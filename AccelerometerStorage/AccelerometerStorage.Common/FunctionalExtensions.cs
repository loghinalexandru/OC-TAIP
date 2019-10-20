using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace AccelerometerStorage.Common
{
    public static class FunctionalExtensions
    {
        public static async Task<Maybe<T>> ToMaybe<T>(this Task<T> task) => await task;

        public static Maybe<T> ToMaybe<T>(this T subject) => subject;

        public static Result<T> EnsureExists<T>(this T subject, string error) => Result.SuccessIf(subject != null, subject, error);

        public static Result<string> EnsureIsValidString(this string subject, string errorMessage)
            => Result.SuccessIf(!string.IsNullOrEmpty(subject), subject, errorMessage);
    }
}
