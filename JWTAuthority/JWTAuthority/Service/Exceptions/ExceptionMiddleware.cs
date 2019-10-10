﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace JWTAuthority.Service.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AggregateException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, AggregateException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errorMessages = new List<string>();

            foreach(var ex in exception.InnerExceptions)
            {
                errorMessages.Add(ex.Message);
            }

            return context.Response.WriteAsync(new ErrorDetails()
            {
                ErrorMessages = errorMessages

            }.ToString());
        }
    }
}
