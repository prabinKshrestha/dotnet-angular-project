using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
