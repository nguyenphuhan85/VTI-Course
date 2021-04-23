using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMiddleware
{
    public static class LoggerMiddlewareExtension
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<LoggerMiddleware>();
        }
    }
}
