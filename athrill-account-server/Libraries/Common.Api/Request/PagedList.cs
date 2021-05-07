using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Api.Request
{
    public interface IPagedList
    {
        int Top { get; set; }
        int Skip { get; set; }
    }
}
