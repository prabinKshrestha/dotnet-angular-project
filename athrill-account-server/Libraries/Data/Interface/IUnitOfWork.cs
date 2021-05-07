using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit(bool logRecords = true);
    }
}
