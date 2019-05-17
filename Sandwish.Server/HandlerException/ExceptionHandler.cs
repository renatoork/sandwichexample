using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandwish.Server.HandlerException
{
    public static class ExceptionHandler
    {
        public static void UseExceptionHandlerLocalizer(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        context = builder.HandlerCodeException(context);
                        var err = builder.HandlerLocalizerException(context);

                        await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                    });
            });

        }
    }
}
