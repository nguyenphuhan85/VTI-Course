using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIMiddleware
{
    public static class RequestMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<RequestMiddleware>();
        }
    }
}
