using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sandwish.Server.HandlerException
{
    public static class HandlerCodeExceptionExtension
    {
        public static HttpContext HandlerCodeException(this IApplicationBuilder builder, HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>();
            if (exception != null)
            {
                var exceptionType = exception.Error;
                var code = HttpStatusCode.InternalServerError; 
                if (exceptionType is InvalidOperationException ||
                    exceptionType is ArgumentException ||
                    exceptionType is ArgumentNullException ||
                    exceptionType is ArgumentOutOfRangeException)
                {
                    code = HttpStatusCode.BadRequest;
                }
                else
                {
                    if (exceptionType is NotImplementedException)
                    {
                        code = HttpStatusCode.NotImplemented;
                    }
                    else
                    {
                        code = HttpStatusCode.InternalServerError;
                    }
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
            }
            return context;
        }
    }
}
