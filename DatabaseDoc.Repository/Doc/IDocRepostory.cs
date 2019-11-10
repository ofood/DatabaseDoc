using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DatabaseDoc.Core;
using DatabaseDoc.Repository.Doc.Entities;

namespace DatabaseDoc.Repository.Doc
{
    /// <summary>
    /// 数据库文档仓储
    /// </summary>
    public interface IDocRepostory:IRepository<TableInfo>
    {
        Task<IEnumerable<TableFieldInfo>> GetFieldInfosAsync(int tableId);
        Task<int> GetCountAsync(string keywords = null, int existnull = 0);
        Task<IEnumerable<TableInfo>> GetAllAsync(int pageindex, int pagesize, string keywords = null, int existnull = 0);
    }
}
