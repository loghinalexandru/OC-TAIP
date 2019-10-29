using JWTAuthority.API.Models;

namespace JWTAuthority.Service
{
    public interface IRegisterService
    {
        void Register(RegisterModel model);
    }
}
