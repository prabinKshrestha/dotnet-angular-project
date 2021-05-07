using AT.Common.Api.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AT.Common.Api.Constants.ApiConstants;

namespace AT.Common.Api.Attributes
{
    public class ODataQueryCapabilitiesAttribute : ActionFilterAttribute
    {
        private readonly ODataCapabilities[] Filters;
        public ODataQueryCapabilitiesAttribute()
        {
            Filters = null;
        }
        public ODataQueryCapabilitiesAttribute(params ODataCapabilities[] filters)
        {
            Filters = filters;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            HttpContext httpContext = context.HttpContext;
            if (!httpContext.Items.ContainsKey(RequestProperties.ODATA_CAPABILITIES))
            {
                if(Filters != null)
                {
                    httpContext.Items.Add(RequestProperties.ODATA_CAPABILITIES, Filters.ToList());
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
