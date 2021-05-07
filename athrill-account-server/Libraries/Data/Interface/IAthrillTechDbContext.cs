using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data.Interface
{
    public interface IAthrillTechDbContext
    {
        void Commit(bool logRecord = true);
    }
}
