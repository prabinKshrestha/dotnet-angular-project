using AT.Common.Constants;
using AT.Common.Enum;
using AT.Common.Infrastructure.Interfaces;
using AT.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Detection;
using static AT.Common.Api.Constants.ApiConstants;

namespace AthrillAccount.Project.Middlewares
{
    public class ClientInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public ClientInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context
            , IClientInfoContext clientInfoContext
            , IDetection detection)
        {
            ClientApplication clientApplication;
            if (context.Request.Headers.ContainsKey(RequestHeaderProperties.APP_CONTEXT) && ATAppContext.ClientApplicationEnumHeaderValuePair.ContainsKey(context.Request.Headers[RequestHeaderProperties.APP_CONTEXT])) 
            {
                clientApplication = ATAppContext.ClientApplicationEnumHeaderValuePair[context.Request.Headers[RequestHeaderProperties.APP_CONTEXT]];
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }
            clientInfoContext.SetClientInfoContext(new ClientInfoContextModel()
            {
                IPAddress = context.Connection?.RemoteIpAddress?.ToString(),
                Device = detection.Device?.Type.ToString(),
                Browser = $"{detection.Browser?.Type}",
                BroswerVersion = detection.Browser?.Version?.ToString(),
                RouteUrl = string.Concat(context.Request.Scheme
                                        , "://"
                                        , context.Request.Host.ToUriComponent()
                                        , context.Request.PathBase.ToUriComponent()
                                        , context.Request.Path.ToUriComponent()
                                        , context.Request.QueryString.ToUriComponent()),
                ClientApplication = clientApplication,
                BatchId = Guid.NewGuid()
            });

            await _next(context);
        }
    }
}
