using AT.Common.Api.Request;
using AT.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AT.Data.Request
{
    public static class PaginationHelper
    {
        public static IList<TEntity> FinalizeQuery<TEntity>(this IQueryable<TEntity> query, RequestBase request) where TEntity : class
        {
            return FinalizeQuery(query as IOrderedQueryable<TEntity>, request);   // to finalize query for only IOrderedQueryable type
        }


        public static IList<TEntity> FinalizeQuery<TEntity>(this IOrderedQueryable<TEntity> query, RequestBase request) where TEntity : class
        {
            // if no paging is true then there should not be any thing related to skip and top
            return request != null && !request.QueryOptions.NoPaging ? new PagedList<TEntity>(query, request.Top, request.Skip) : new PagedList<TEntity>(query);
        }



        public static int GetCount<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            return query.Count();
        }



        public static TEntity FinalizeQueryById<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity,bool>> predicate, int id) where TEntity : class
        {
            TEntity entity =  query.FirstOrDefault(predicate);
            if(entity == null)
            {
                throw new ATReferenceException(id,typeof(TEntity).Name);
            }
            return entity;
        }
    }



    public class PagedList<TEntity> : List<TEntity>, IPagedList where TEntity : class
    {
        public int Top { get; set; }
        public int Skip { get; set; }
        public PagedList(IOrderedQueryable<TEntity> source)
        {
            AddRange(source.ToList());   // uses List
        }
        public PagedList(IOrderedQueryable<TEntity> source, int? top, int? skip)
        {
            SetPage(top,skip);
            List<TEntity> entities = top != null ? source.Skip(Skip).Take(Top).ToList() : source.ToList();   // when no paging = true is passeed then only top is null
            AddRange(entities);
        }
        private void SetPage(int? top, int? skip)
        {
            Top = top ?? 1000; 
            Skip = skip ?? 0;
        }
    }
}
