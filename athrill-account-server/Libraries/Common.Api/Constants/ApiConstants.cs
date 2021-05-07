using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Api.Constants
{
    public static class ApiConstants
    {
        public static class UriODataQueryConstants
        {
            public const string EXPAND = "$expand";
            public const string SELECT= "$select";
            public const string SEARCH = "$search";
            public const string SKIP = "$skip";
            public const string TOP = "$top";
            public const string NOPAGING = "$nopaging";
            public const string ORDERBY = "$orderby";
            public const string FILTER = "$filter";
        }
        public static class RequestProperties
        {
            public const string ODATA_OPTIONS = "ODataQueryOptions";
            public const string ODATA_CAPABILITIES = "ODataQueryCapabilities";
        }
        public static class RequestHeaderProperties
        {
            public const string APP_CONTEXT = "x-app-context";
        }
        public static class ResponseHeaderProperties
        {
            public const string COUNT = "x-count";
        }
    }
}
