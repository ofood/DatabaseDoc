using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DatabaseDoc.Core.DbProvider;

namespace DatabaseDoc.Core
{
    public  class DapperDbContext : IDbContext
    {
        private IDbManager _dbManager;
        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private int? _commandTimeout = null;

        public DapperDbContext(IDbManager dbManager)
        {
            _dbManager = dbManager;
            _dbConnection = dbManager.CreateConnection("test");

        }
        public bool IsTransactionStarted { get; private set; }

        //protected abstract IDbConnection CreateConnection(string connectionString);

        public void BeginTransaction()
        {
            if (IsTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
            IsTransactionStarted = true;
        }
        #region Dapper Execute & Query

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.ExecuteAsync(sql, param, _dbTransaction, _commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.QueryAsync<T>(sql, param, _dbTransaction, _commandTimeout, commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, param, _dbTransaction, _commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.QueryAsync(sql, map, param, _dbTransaction, true, splitOn, _commandTimeout, commandType);
        }

        #endregion Dapper Execute & Query
        public void Commit()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _dbTransaction.Commit();
            _dbTransaction = null;

            IsTransactionStarted = false;

        }

        public void Dispose()
        {
            if (IsTransactionStarted)
                Rollback();

            _dbConnection.Close();
            _dbConnection.Dispose();
            _dbConnection = null;
        }

        public void Rollback()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;

            IsTransactionStarted = false;
        }
    }
}
