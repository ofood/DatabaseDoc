using DatabaseDoc.Domain.Doc.Dtos;
using DatabaseDoc.Repository.Doc.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDoc.Domain.Doc
{
    public interface IDocService
    {
        Task<IEnumerable<TableInfo>> GetTables(SearchConditionParams query);
        Task<IEnumerable<TableFieldInfo>> GetFields(int tableid);
        Task<int> GetRecordCounts(SearchConditionParams query);
    }
}
