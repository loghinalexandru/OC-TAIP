using System;

namespace JWTAuthority.Service.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {

        }

        public ValidationException(String message): base(message)
        {

        }

        public ValidationException(String message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
