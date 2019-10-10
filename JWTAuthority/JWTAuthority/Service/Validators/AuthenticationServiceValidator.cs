﻿using FluentValidation;
using JWTAuthority.DataAccess.Repository;
using JWTAuthority.Helpers;
using JWTAuthority.Models;

namespace JWTAuthority.Service.Validators
{
    public class AuthenticationServiceValidator : AbstractValidator<AuthorizationModel>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationServiceValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(model => model.Username)
                .NotEmpty()
                .WithMessage("Username can not be null!")
                .Must(BeValidUsername)
                .WithMessage("Username does not exist!");
        }

        private bool BeValidUsername(string username)
        {
            return
                _userRepository.GetByUsername(username) != null;
        }
    }
}
