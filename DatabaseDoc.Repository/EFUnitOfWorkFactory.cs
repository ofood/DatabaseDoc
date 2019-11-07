using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Core
{
    public class EFUnitOfWorkFactory:IUnitOfWorkFactory
    {
        private readonly EFDbContext _efDbContext;
        public EFUnitOfWorkFactory(EFDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }
        public IUnitOfWork Create()
        {
            return new UnitOfWork(_efDbContext);
        }
    }
}
