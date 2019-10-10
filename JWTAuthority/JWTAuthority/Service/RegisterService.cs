using JWTAuthority.API.Models;
using JWTAuthority.DataAccess.Repository;
using JWTAuthority.Domain;
using JWTAuthority.Helpers;

namespace JWTAuthority.Service
{
    public class RegisterService : IRegisterService
    {

        private readonly IHashHelper _hashHelper;
        private readonly IUserRepository _userRepository;

        public RegisterService(IHashHelper hashHelper, IUserRepository userRepository)
        {
            _hashHelper = hashHelper;
            _userRepository = userRepository;
        }

        public void Register(RegisterModel model)
        {
            var newUser = new User
            {
                Username = model.Username,
                Password = _hashHelper.GetHashAsString(model.Password),
                Email = model.Email
            };

            _userRepository.AddUser(newUser);
        }
    }
}
