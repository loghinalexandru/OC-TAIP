using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JWTAuthority.Service.Validators
{
    public class ValidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext,
            ValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                var exceptionList = result.Errors.Select(error => new ValidationException(error.ErrorMessage)).ToList();

                throw new AggregateException(exceptionList);
            }

            return result;
        }

        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext,
            ValidationContext validationContext)
        {
            return validationContext;
        }
    }
}