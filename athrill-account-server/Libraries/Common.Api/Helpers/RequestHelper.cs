using AT.Common.Api.Infrastructure;
using AT.Common.Api.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Api.Helpers
{
    public static class RequestHelper
    {
        public static RequestBase GetRequestBase(ODataQueryOptions oDataQueryOptions)
        {
            if(oDataQueryOptions == null)
            {
                return null;
            }
            RequestBase requestBase = new RequestBase();
            requestBase.SetQueryOptions(oDataQueryOptions);
            return requestBase;
        }
        public static GetByIdRequestBase GetByIdRequestBase(ODataQueryOptions oDataQueryOptions, int id)
        {
            if (oDataQueryOptions == null)
            {
                return null;
            }
            GetByIdRequestBase getByIdRequestBase = new GetByIdRequestBase() { 
                Id = id
            };
            getByIdRequestBase.SetQueryOptions(oDataQueryOptions);
            return getByIdRequestBase;
        }
    }
}
