using System;

namespace JWTAuthority.Helpers
{
    public interface ITokenBuilder
    {
        public String GetToken(String username);
    }
}
