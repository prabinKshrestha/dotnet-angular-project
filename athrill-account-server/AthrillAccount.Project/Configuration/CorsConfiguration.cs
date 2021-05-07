using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using static AT.Common.Api.Constants.ApiConstants;

namespace AthrillAccount.Project.Configuration
{
    public static class  CorsConfiguration
    {
        public static string[] GetAllowedOrigins(IWebHostEnvironment env, IConfiguration configuration)
        {
            List<string> retVal = configuration.GetValue<string>("ATAppSettings:AllowOriginSites").Split(",").ToList();
            if (env.IsDevelopment())
            {
                retVal.AddRange(new List<string>() {
                    "http://localhost:4200",
                    "http://localhost:6001",
                    "http://localhost:6002"
                });
            }
            return retVal.ToArray();
        }
        public static string[] GetExposedHeaders()
        {
            List<string> retVal = new List<string>()
            {
                ResponseHeaderProperties.COUNT
            };
            return retVal.ToArray();
        }
    }
}
