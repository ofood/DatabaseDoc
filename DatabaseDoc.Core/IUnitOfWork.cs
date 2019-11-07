using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace DatabaseDoc.Core
{
    /// <summary>
    /// 工作单元
    /// 维护受业务事务影响的对象列表，并协调变化的写入和并发问题的解决
    /// </summary>
    public interface IUnitOfWork :IDisposable
    {
        void SaveChanges();
    }
}
