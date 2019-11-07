using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseDoc.Core
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
