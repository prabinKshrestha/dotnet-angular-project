using AT.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AT.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AthrillTechDbContext _context;
        public UnitOfWork(AthrillTechDbContext context)
        {
            _context = context;
        }
        public void Commit(bool logRecords = true)
        {
            _context.Commit(logRecords);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
