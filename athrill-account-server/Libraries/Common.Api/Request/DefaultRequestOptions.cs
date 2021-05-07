using AT.Common.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Common.Api.Request
{
    public class DefaultRequestOptions
    {
        public const int DEFAULT_SKIP = 0;
        public const int DEFAULT_PAGE_SIZE = 200; // default page size is 200, so that 200 records are fetched if top is not sent
        public const int MAX_PAGE_SIZE = 1000; // maximum fetch 1000 records only

        // following property are repeated so that they can be used in areas different than odata areas
        public int? Skip { get; set; } // here skip is not page index, instead it is the number of records to be skipped
        public int? Top { get; set; } // it works as page size that is number of record to be shown per page
        public bool DoPaging => Skip.HasValue || Top.HasValue;
        public string SearchText { get; set; }
        public ODataQueryOptions QueryOptions { get; set; } // this is set only for odata operations
        public bool AsNoTrackingEnabled = true;
        public void SetQueryOptions(ODataQueryOptions queryOptions)
        {
            QueryOptions = queryOptions;
            SearchText = SearchText ?? QueryOptions.Search;
            Top = Math.Min(QueryOptions.Top ?? DEFAULT_PAGE_SIZE, MAX_PAGE_SIZE);  // if: top is null then set default page size; else: take top value if: less than max page size; else: take max page size
            Skip = QueryOptions.Skip ?? DEFAULT_SKIP; // if skip is null then provide default page index = 0
        }

    }
}
