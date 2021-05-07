using AT.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using AT.Common.Exceptions;
using AT.Common.Enum;
using AT.Entity.System.ATDatas;

namespace AT.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AthrillTechDbContext _context;
        private readonly DbSet<TEntity> table;
        public Repository(AthrillTechDbContext context)
        {
            _context = context;
            table = _context.Set<TEntity>();
        }
        public IQueryable<TEntity> Table => table.AsQueryable();
        public IQueryable<TEntity> TableNotTracked => table.AsNoTracking();

        public IEnumerable<TEntity> GetAll()
        {
            return table.ToList();
        }
        public TEntity Get(int id)
        {
            TEntity entity = table.Find(id);
            if (entity == null)
            {
                throw new ATReferenceException(id, typeof(TEntity).Name); // throw object not found error
            }
            return entity;
        }

        public void Add(TEntity entity)
        {
            table.Add(entity);
        }
        public void Update(TEntity entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            table.Remove(entity);
        }

        public bool VerifyByReference(int id)
        {
            TEntity entity = table.Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
                return true;
            }
            return false;
        }

        public bool VerifyByReferenceForATDataValues(int id, ATDataTypes aTDataTypes)
        {
            ATDataValue entity = table.Find(id) as ATDataValue;
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
                return entity.ATDataTypeId == (int)aTDataTypes;
            }
            return false;
        }
    }
}
