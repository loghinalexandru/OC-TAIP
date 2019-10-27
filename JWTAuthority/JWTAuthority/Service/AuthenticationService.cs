using JWTAuthority.DataAccess.Repository;
using JWTAuthority.Domain;
using JWTAuthority.Helpers;
using JWTAuthority.Models;
using JWTAuthority.Service.Validators;
using System;

namespace JWTAuthority.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenBuilder _tokenBuilderService;
        private readonly IHashHelper _hashHelper;

        public AuthenticationService(IUserRepository userRepository, ITokenBuilder tokenBuilderService, IHashHelper hashHelper)
        {
            _userRepository = userRepository;
            _tokenBuilderService = tokenBuilderService;
            _hashHelper = hashHelper;
        }

        public String Authenticate(AuthorizationModel model)
        {
            if (IsValidUser(model))
            {
                return _tokenBuilderService.GetToken(_userRepository.GetByUsername(model.Username));
            }
            else
            {
                return null;
            }
        }

        private bool IsValidUser(AuthorizationModel model)
        {
            var user = _userRepository.GetByUsername(model.Username);

            return
                model.Username == user.Username &&
                _hashHelper.GetHashAsString(model.Password) == user.Password;
        }
    }
}
