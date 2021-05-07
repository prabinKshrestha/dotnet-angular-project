using System;
using System.Collections.Generic;
using System.Text;
using static AT.Common.Api.Constants.ApiConstants;

namespace AT.Common.Api.Infrastructure
{
    public static class ODataQueryCapabilities
    {
        public static Dictionary<ODataCapabilities, string> ODATA_QUERY_CAPABILITIES_CONSTANTS = new Dictionary<ODataCapabilities, string>()
        {
            {ODataCapabilities.NoExpand, UriODataQueryConstants.EXPAND},
            {ODataCapabilities.NoFilter, UriODataQueryConstants.FILTER},
            {ODataCapabilities.NoSelect, UriODataQueryConstants.SELECT},
            {ODataCapabilities.NoOrderBy, UriODataQueryConstants.ORDERBY},
            {ODataCapabilities.NoSearch, UriODataQueryConstants.SEARCH},
            {ODataCapabilities.NoTop, UriODataQueryConstants.TOP},
            {ODataCapabilities.NoSkip, UriODataQueryConstants.SKIP},
            {ODataCapabilities.NoPaging, UriODataQueryConstants.NOPAGING}
        };

        public static Dictionary<ODataCapabilities, string> ODATA_QUERY_CAPABILITIES_NAME_CONSTANTS_DATATYPE = new Dictionary<ODataCapabilities, string>()
        {
            {ODataCapabilities.NoExpand, "string"},
            {ODataCapabilities.NoFilter, "string"},
            {ODataCapabilities.NoSelect, "string"},
            {ODataCapabilities.NoOrderBy, "string"},
            {ODataCapabilities.NoSearch, "string"},
            {ODataCapabilities.NoTop, "int"},
            {ODataCapabilities.NoSkip, "int"},
            {ODataCapabilities.NoPaging, "bool"}
        };
    }
    public enum ODataCapabilities
    {
        NoExpand = 1,
        NoFilter,
        NoSelect,
        NoOrderBy,
        NoSearch,
        NoTop,
        NoSkip,
        NoPaging
    }
}