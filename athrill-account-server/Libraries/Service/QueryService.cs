using AT.Common.Api.Request;
using AT.Data.Request;
using AT.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using AT.Common.Api.Infrastructure;

namespace AT.Service
{
    public abstract class QueryService<TEntity> : IQueryService<TEntity> where TEntity : class
    {
        protected IRepository<TEntity> QueryRepository { get; set; }
        protected OrderByItem DefaultOrderByItem { get; set; }
        public QueryService()
        {
            SetRepository();
            SetDefaultOrderByItem();
        }
        public abstract void SetRepository();
        public abstract void SetDefaultOrderByItem();
        public abstract TEntity GetById(int id, GetByIdRequestBase request = null);
        public virtual IEnumerable<TEntity> GetAll(RequestBase request = null)
        {
            return GetQuery(request).FinalizeQuery(request);
        }
        public virtual int GetCount(RequestBase request = null)
        {
            return GetQuery(request).GetCount();
        }
        public virtual IQueryable<TEntity> GetQuery(RequestBase request)
        {
            if (QueryRepository == null)
            {
                SetRepository();
            }
            IQueryable<TEntity> query = QueryRepository.TableFromRequest(request);
            IOrderedQueryable<TEntity> orderedQuery = query.OrderFromRequest(request, DefaultOrderByItem);
            return orderedQuery;
        }
        public virtual IQueryable<TEntity> GetQueryForId(GetByIdRequestBase request)
        {
            if (QueryRepository == null)
            {
                SetRepository();
            }
            IQueryable<TEntity> query = QueryRepository.TableFromRequestById(request);
            return query;
        }
    }
}
