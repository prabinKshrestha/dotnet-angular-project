using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Service
{
    public interface IBaseServiceRule<TEntity> where TEntity : class
    {
        void CheckAddRule(TEntity entity);
        void CheckUpdateRule(TEntity entity);
        void CheckDeleteRule(TEntity entity);
    }
}
