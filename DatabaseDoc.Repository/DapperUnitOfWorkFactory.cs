using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Core
{
    public class DapperUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DapperDbContext _dapperDbContext;
        public DapperUnitOfWorkFactory(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_dapperDbContext);
        }
    }
}
