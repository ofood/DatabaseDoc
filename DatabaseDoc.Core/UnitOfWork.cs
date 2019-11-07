using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.BeginTransaction();
        }
        public void SaveChanges()
        {
            if(!_dbContext.IsTransactionStarted)
                throw new InvalidOperationException("Transaction have already been commited or disposed.");
            _dbContext.Commit();
        }
        public void Dispose()
        {
            if (_dbContext.IsTransactionStarted)
                _dbContext.Rollback();
        }
    }
}
