using AT.Common.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using AT.Common.Api.Infrastructure;

namespace AT.Data.Request
{
    public static class OrderQueryHelper
    {
        public static IOrderedQueryable<TEntity> OrderFromRequest<TEntity>(this IQueryable<TEntity> query, 
                                                                                RequestBase request,   
                                                                                params OrderByItem[] orderByItems) 
            where TEntity : class
        {
            if(request != null && request.QueryOptions.IsOrderByDefined)
            {
                query = query.OrderBy(request.QueryOptions.OrderByClause.ToString());
            }
            else if(orderByItems.Count() > 0) // if there is no oder by passed from client then set default order by
            {
                query = query.OrderBy(new OrderByClause(orderByItems).ToString());
            }
            return query as IOrderedQueryable<TEntity>;
        }
    }
}
