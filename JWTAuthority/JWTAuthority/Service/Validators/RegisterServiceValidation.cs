using FluentValidation;
using JWTAuthority.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthority.Service.Validators
{
    public class RegisterServiceValidation : AbstractValidator<RegisterModel>
    {
        public RegisterServiceValidation()
        {
            RuleFor(model => model.Username)
                .MinimumLength(3)
                .WithMessage("Username is too short, minimum length is 3 characters!");

            RuleFor(model => model.Password)
                .MinimumLength(8)
                .WithMessage("Password is too short, minimum length is 8 characters!");
        }
    }
}
