using FluentValidation;
using JWTAuthority.API.Models;
using JWTAuthority.DataAccess.Repository;

namespace JWTAuthority.Service.Validators
{
    public class RegisterServiceValidator : AbstractValidator<RegisterModel>
    {

        private readonly IUserRepository _userRepository;

        public RegisterServiceValidator(IUserRepository userRepository)
        {

            _userRepository = userRepository;

            RuleFor(model => model.Username)
                .MinimumLength(3)
                .WithMessage("{PropertyName}:Username is too short, minimum length is 3 characters!")
                .Must(BeValidUsername)
                .WithMessage("{PropertyName}:Username already taken!");

            RuleFor(model => model.Password)
                .MinimumLength(8)
                .WithMessage("{PropertyName}:Password is too short, minimum length is 8 characters!");

            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("{PropertyName}:Not a valid email address!");
        }

        private bool BeValidUsername(string username)
        {
            return 
                _userRepository.IsAvailableUsername(username);
        }
    }
}
