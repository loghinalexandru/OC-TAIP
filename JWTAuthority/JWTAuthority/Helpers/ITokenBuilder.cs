using JWTAuthority.Domain;
using System;

namespace JWTAuthority.Helpers
{
    public interface ITokenBuilder
    {
        String GetToken(User user);
    }
}
