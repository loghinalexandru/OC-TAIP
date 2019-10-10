using JWTAuthority.Models;
using System;

namespace JWTAuthority.Service
{
    public interface IAuthenticationService
    {
        public String Authenticate(AuthorizationModel model);
    }
}
