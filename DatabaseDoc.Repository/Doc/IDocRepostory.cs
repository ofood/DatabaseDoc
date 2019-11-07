using System;
using System.Collections.Generic;
using System.Text;
using DatabaseDoc.Core;
using DatabaseDoc.Repository.Doc.Entities;

namespace DatabaseDoc.Repository.Doc
{
    /// <summary>
    /// 数据库文档仓储
    /// </summary>
    public interface IDocRepostory:IRepository<TableInfo>
    {
    }
}
