using AT.Common.Api.Infrastructure;
using AT.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AT.Common.Api.Constants.ApiConstants;

namespace AT.Common.Api.Attributes
{
    public class ODataQueryFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasODataQueryCapabilities = context.Filters.Any(x => x.GetType() == typeof(ODataQueryCapabilitiesAttribute));
            // only process when there is ODataQueryCapabilities Attribute for the method
            if (hasODataQueryCapabilities)
            {
                HttpContext httpContext = context.HttpContext;
                IQueryCollection queryCollection = httpContext.Request.Query;
                if (httpContext.Items.ContainsKey(RequestProperties.ODATA_CAPABILITIES))
                {
                    CheckODataQueryViolation(queryCollection, (List<ODataCapabilities>)httpContext.Items[RequestProperties.ODATA_CAPABILITIES]);
                }

                string expandQuery = GetODataQueryValue(queryCollection, UriODataQueryConstants.EXPAND);
                //string selectQuery = GetODataQueryValue(queryCollection, UriODataQueryConstants.SELECT);
                string filterQuery = GetODataQueryValue(queryCollection, UriODataQueryConstants.FILTER);
                string orderByQuery = GetODataQueryValue(queryCollection, UriODataQueryConstants.ORDERBY);
                string searchText = GetODataQueryValue(queryCollection, UriODataQueryConstants.SEARCH);
                int? top = null;
                if (queryCollection.ContainsKey(UriODataQueryConstants.TOP)) 
                {
                    top = Convert.ToInt32(GetODataQueryValue(queryCollection, UriODataQueryConstants.TOP));
                }
                int? skip = null;
                if (queryCollection.ContainsKey(UriODataQueryConstants.SKIP))
                {
                    skip = Convert.ToInt32(GetODataQueryValue(queryCollection, UriODataQueryConstants.SKIP));
                }
                bool noPaging = Convert.ToBoolean(GetODataQueryValue(queryCollection, UriODataQueryConstants.NOPAGING));

                ExpandClause expandClause = ParseExpandQuery(expandQuery);
                FilterClause filterClause = ParseFilterQuery(filterQuery);
                OrderByClause orderByClause = ParseOrderByQuery(orderByQuery);
                //SelectClause selectClause = ParseSelectQuery(selectQuery);

                ODataQueryOptions oDataQueryOptions = new ODataQueryOptions
                {
                    ExpandClause = expandClause,
                    FilterClause = filterClause,
                    OrderByClause = orderByClause,
                    //SelectClause = selectClause,
                    Top = top,
                    Skip = skip,
                    Search = searchText,
                    NoPaging = noPaging
                };

                if (!httpContext.Items.ContainsKey(RequestProperties.ODATA_OPTIONS))
                {
                    httpContext.Items.Add(RequestProperties.ODATA_OPTIONS, oDataQueryOptions);
                }
            }
            base.OnActionExecuting(context);
        }


        #region Parse
        private ExpandClause ParseExpandQuery(string expandQuery)
        {
            if(expandQuery == null)
            {
                return null;
            }
            ExpandClause expandClause = new ExpandClause();
            List<string> expandList = expandQuery.Split(',').Select(x => x.Trim()).ToList();
            if (expandList.Any())
            {
                expandClause.ExpandItems = expandList;
            }
            return expandClause;
        }
        private FilterClause ParseFilterQuery(string filterQuery)
        {
            if (filterQuery == null)
            {
                return null;
            }
            FilterClause filterClause = new FilterClause
            {
                FilterItems = new List<string>() { filterQuery }
            };
            return filterClause;
        }
        private SelectClause ParseSelectQuery(string selectQuery)
        {
            if (selectQuery == null)
            {
                return null;
            }
            SelectClause selectClause = new SelectClause();
            List<string> selectList = selectQuery.Split(',').Select(x => x.Trim()).ToList();
            if (selectList.Any())
            {
                selectClause.SelectItems = selectList;
            }
            return selectClause;
        }

        private OrderByClause ParseOrderByQuery(string orderByQuery)
        {
            if (orderByQuery == null)
            {
                return null;
            }
            OrderByClause orderByClause = new OrderByClause();
            List<string> orderList = orderByQuery.Split(',').Select(x => x.Trim()).ToList();
            if (orderList.Any())
            {
                List<OrderByItem> orderByItems = new List<OrderByItem>();
                orderList.ForEach(x =>{
                    List<string> orderByPhrase= x.Split(' ').Select(y => y.Trim()).ToList();
                    orderByPhrase.RemoveAll(r => r.Trim() == ""); // remove space because there might be muliple space between words
                    if(orderByPhrase.Count() == 1)
                    {
                        orderByItems.Add(new OrderByItem(orderByPhrase.First())); // if there is only one word then default is ascending
                    }
                    else if(orderByPhrase.Count() == 2)
                    {
                        orderByItems.Add(new OrderByItem(orderByPhrase.First(), orderByPhrase.Last())); // if there is only two word then,  passing both
                    }
                });
                orderByClause.OrderByItems = orderByItems;
            }
            return orderByClause;
        }

        #endregion
        private void CheckODataQueryViolation(IQueryCollection queryCollection, List<ODataCapabilities> oDataQueryCapabilitiesEnums)
        {
            foreach (ODataCapabilities enums in oDataQueryCapabilitiesEnums)
            {
                string hello = ODataQueryCapabilities.ODATA_QUERY_CAPABILITIES_CONSTANTS[enums];
                if (queryCollection.ContainsKey(ODataQueryCapabilities.ODATA_QUERY_CAPABILITIES_CONSTANTS[enums]))
                {
                    throw new ATODataException("Provided OData Query Parameter cannot be Used for the endpoint.");
                }
            }
        }

        private string GetODataQueryValue(IQueryCollection queryCollection, string queryConstant)
        {
            if (queryCollection.TryGetValue(queryConstant, out StringValues retVal))
            {
                return retVal.ToString();
            }
            return null;
        }
    }
}
