using DatabaseDoc.Core.DbProvider;
using DatabaseDoc.Repository.Doc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseDoc.Domain.Doc.Impl
{
    
    public class DocService:IDocService
    {
        private readonly IDbManager _dbManager;
        private readonly ILogger _logger;
        private readonly IDbConnection _conn;
        private readonly IDocRepostory _docRepostory;
        public DocService(IDbManager dbManager, ILogger<DocService> logger,IDocRepostory docRepostory)
        {
            //_dbManager = dbManager;
            _logger = logger;
            _docRepostory = docRepostory;
            //_conn = _dbManager.CreateConnection("test");
        }
        ~DocService()
        {
            _conn.Dispose();
        }
    }
}
