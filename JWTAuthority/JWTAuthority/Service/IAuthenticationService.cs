using JWTAuthority.Models;
using System;

namespace JWTAuthority.Service
{
    public interface IAuthenticationService
    {
        string Authenticate(AuthorizationModel model);
    }
}
