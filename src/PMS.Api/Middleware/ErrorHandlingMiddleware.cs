﻿using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Extensions;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace PMS.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;


        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }



        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }



        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var exType = ex.GetType();

            switch (ex)
            {
                case Exception when exType == typeof(ArgumentException): code = HttpStatusCode.BadRequest; break;

                case Exception when exType == typeof(ArgumentNullException): code = HttpStatusCode.BadRequest; break;

                case Exception when exType == typeof(UnauthorizedAccessException): code = HttpStatusCode.Unauthorized; break;

                case Exception when exType == typeof(InvalidOperationException): code = HttpStatusCode.Conflict; break;

                case Exception when exType == typeof(UserNotFoundException): code = HttpStatusCode.NotFound; break;

                case Exception when exType == typeof(EmailAlreadyExistException): code = HttpStatusCode.BadRequest; break;
                    
                case Exception when exType == typeof(InvalidCredentialException): code = HttpStatusCode.BadRequest; break;
            }

            var result = JsonSerializer.Serialize(new { Error = ex.Message, Code = code });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}