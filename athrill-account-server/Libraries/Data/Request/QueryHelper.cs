using AT.Common.Api.Request;
using AT.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace AT.Data.Request
{
    public static class QueryHelper
    {
        #region Get All

        public static IQueryable<TEntity> TableFromRequest<TEntity>(this IRepository<TEntity> repository,RequestBase request) where TEntity : class
        {
            IQueryable<TEntity> query = (bool)request?.AsNoTrackingEnabled ?
                                            repository.TableNotTracked.ProcessRequestBase(request) :
                                            repository.Table.ProcessRequestBase(request);
            return query;
        }
        public static IQueryable<TEntity> ProcessRequestBase<TEntity>(this IQueryable<TEntity> query, RequestBase request) where TEntity : class
        {
            if (request != null && request.QueryOptions != null)
            {
                // expand
                if (request.QueryOptions.IsExpandDefined)
                {
                    foreach (string expand in request.QueryOptions.ExpandClause.ExpandItems)
                    {
                        query = query.Include(expand);
                    }
                }
                // filter
                if (request.QueryOptions.IsFilterDefined)
                {
                    query = query.Where(request.QueryOptions.FilterClause.FilterItems.FirstOrDefault());
                }
            }
            return query;
        }

        #endregion

        // though these methods are  same, theses are separated so that we can later control here in code level fo both areas

        #region Get By Id

        public static IQueryable<TEntity> TableFromRequestById<TEntity>(this IRepository<TEntity> repository, GetByIdRequestBase request) where TEntity : class
        {
            IQueryable<TEntity> query = (bool)request?.AsNoTrackingEnabled ?
                                            repository.TableNotTracked.ProcessRequestBaseById(request) :
                                            repository.Table.ProcessRequestBaseById(request);
            return query;
        }
        public static IQueryable<TEntity> ProcessRequestBaseById<TEntity>(this IQueryable<TEntity> query, GetByIdRequestBase request) where TEntity : class
        {
            if (request != null && request.QueryOptions != null)
            {
                // expand
                if (request.QueryOptions.IsExpandDefined)
                {
                    foreach (string expand in request.QueryOptions.ExpandClause.ExpandItems)
                    {
                        query = query.Include(expand);
                    }
                }
                // filter
                if (request.QueryOptions.IsFilterDefined)
                {
                    query = query.Where(request.QueryOptions.FilterClause.FilterItems.FirstOrDefault());
                }
            }
            return query;
        }

        #endregion
    }
}
