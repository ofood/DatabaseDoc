using DatabaseDoc.Core.DbProvider;
using DatabaseDoc.Domain.Doc.Dtos;
using DatabaseDoc.Repository.Doc;
using DatabaseDoc.Repository.Doc.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDoc.Domain.Doc.Impl
{
    
    public class DocService:IDocService
    {
        //private readonly IDbManager _dbManager;
        private readonly ILogger _logger;
        //private readonly IDbConnection _conn;
        private readonly IDocRepostory _docRepostory;
        public DocService(ILogger<DocService> logger,IDocRepostory docRepostory)
        {
            //_dbManager = dbManager;
            _logger = logger;
            _docRepostory = docRepostory;
            //_conn = _dbManager.CreateConnection("test");
        }
        public async Task<IEnumerable<TableInfo>> GetTables(SearchConditionParams query)
        {
            return await _docRepostory.GetAllAsync(query.PageIndex, query.PageSize, query.KeyWords, query.ExistNull);
        }
        public async Task<IEnumerable<TableFieldInfo>> GetFields(int tableid)
        {
            return await _docRepostory.GetFieldInfosAsync(tableid);
        }
        public async Task<int> GetRecordCounts(SearchConditionParams query)
        {
            return await _docRepostory.GetCountAsync(query.KeyWords, query.ExistNull);
        }
    }
}
