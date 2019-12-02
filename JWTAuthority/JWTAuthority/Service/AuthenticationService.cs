using JetBrains.Annotations;
using JWTAuthority.DataAccess.Repository;
using JWTAuthority.Helpers;
using JWTAuthority.Models;

namespace JWTAuthority.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenBuilder _tokenBuilderService;
        private readonly IHashHelper _hashHelper;

        public AuthenticationService(IUserRepository userRepository, ITokenBuilder tokenBuilderService,
            IHashHelper hashHelper)
        {
            _userRepository = userRepository;
            _tokenBuilderService = tokenBuilderService;
            _hashHelper = hashHelper;
        }

        public string Authenticate(AuthorizationModel model)
        {
            return IsValidUser(model)
                ? _tokenBuilderService.GetToken(_userRepository.GetByUsername(model.Username))
                : null;
        }

        [NotNull]
        private bool IsValidUser(AuthorizationModel model)
        {
            var user = _userRepository.GetByUsername(model.Username);

            return
                model.Username == user.Username &&
                _hashHelper.GetHashAsString(model.Password) == user.Password;
        }
    }
}