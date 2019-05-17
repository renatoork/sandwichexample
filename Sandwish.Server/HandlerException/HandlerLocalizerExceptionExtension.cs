using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandwish.Server.HandlerException
{
    public static class HandlerLocalizerExceptionExtension
    {
        public static string HandlerLocalizerException(this IApplicationBuilder builder, HttpContext context)
        {

            var loc = builder.ApplicationServices.GetService<IStringLocalizer<ExceptionLocalizer>>();

            var ex = context.Features.Get<IExceptionHandlerFeature>();
            if (ex != null)
            {
                var param = new List<object>();
                if (ex.Error.Data != null)
                {
                    foreach (var p in ex.Error.Data.Values)
                    {
                        param.Add(p.ToString());
                    }
                }

                return JsonConvert.SerializeObject(new ErrorDto()
                {
                    Message = loc[ex.Error.GetType().Name, param.ToArray()].Value,
                });
            }
            return null;
        }
    }
}
