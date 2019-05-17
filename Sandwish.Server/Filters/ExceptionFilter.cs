using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandwish.Server.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IStringLocalizer<ExceptionFilter> _localizer;
        public ExceptionFilter(IStringLocalizer<ExceptionFilter> localizer)
        {
            _localizer = localizer;
        }
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            string message = _localizer[exception.GetType().Name];
            context.ExceptionHandled = true;
        }
    }
}
