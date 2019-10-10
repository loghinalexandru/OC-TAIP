using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace JWTAuthority.Service.Validators
{
    public class ValidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                var exceptionList = new List<ValidationException>();

                foreach(var error in result.Errors)
                {
                    exceptionList.Add(new ValidationException(error.ErrorMessage));
                }

                throw new AggregateException(exceptionList);
            }

            return result;
        }

        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }
    }
}
