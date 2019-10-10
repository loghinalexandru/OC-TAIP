using System;

namespace JWTAuthority.Helpers
{
    public interface IHashHelper
    {
        public String GetHashAsString(String message);
    }
}
