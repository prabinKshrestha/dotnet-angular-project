using AthrillAccount.Project.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthrillAccount.Project.Configuration
{
    public static class MiddwareConfiguration
    {
        public static void MiddlewareConfiguration(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomResponseHeadersMiddleware>();
            app.UseMiddleware<ClientInfoMiddleware>();
        }
    }
}
