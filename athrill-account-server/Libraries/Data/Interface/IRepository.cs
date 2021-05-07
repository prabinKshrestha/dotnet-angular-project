using AT.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AT.Data.Interface
{
    public interface IRepository<TEntity>  where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNotTracked { get; }
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        bool VerifyByReference(int id);
        bool VerifyByReferenceForATDataValues(int id, ATDataTypes aTDataTypes);
    }
}
