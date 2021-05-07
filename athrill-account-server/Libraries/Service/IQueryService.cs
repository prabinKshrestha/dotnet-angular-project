using AT.Common.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT.Service
{
    public interface IQueryService<TEntity> where TEntity : class
    {
        TEntity GetById(int id, GetByIdRequestBase request = null);
        IEnumerable<TEntity> GetAll(RequestBase request = null);
        int GetCount(RequestBase request = null);
    }
}
