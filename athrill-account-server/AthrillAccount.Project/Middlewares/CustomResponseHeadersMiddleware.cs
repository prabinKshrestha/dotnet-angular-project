using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Middlewares
{
    public class CustomResponseHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            context.Response.Headers.Add("cache-control", "no-cache,no-store,max-age=0,must-revalidate");
            context.Response.Headers.Add("pragma", "no-cache");
            context.Response.Headers.Add("expires", "-1");
            context.Response.Headers.Add("x-frame-options", "SAMEORIGIN");
            //context.Response.Headers.Add("strict-transport-security", "max-age=31536000");
            await _next(context);
        }
    }
}
