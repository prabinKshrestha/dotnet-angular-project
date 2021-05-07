using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Api.Request
{
    public class RequestBase<T> : RequestBase
    {
    }
    public class RequestBase : DefaultRequestOptions
    {
        // place properties here if need to used from different areas. Odataqueryoption should be used for odata purpose only
    }
    public class GetByIdRequestBase : DefaultRequestOptions
    {
        public int Id { get; set; }
    }
}
