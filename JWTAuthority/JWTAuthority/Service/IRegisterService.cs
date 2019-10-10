using JWTAuthority.API.Models;

namespace JWTAuthority.Service
{
    public interface IRegisterService
    {
        public void Register(RegisterModel model);
    }
}
