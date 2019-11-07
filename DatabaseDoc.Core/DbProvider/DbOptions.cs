using System;
using System.Collections.Generic;

namespace DatabaseDoc.Core.DbProvider
{
    public class DbOptions : Dictionary<string, DbSetting>
    {
        public DbOptions():base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }

    public class DbSetting
    {
        /// <summary>
        /// connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// database type 
        /// </summary>
        public DatabaseType DatabaseType { get; set; }
        /// <summary>
        /// 创建数据库连接工厂程序集
        /// </summary>
        public string AssemblyName { get; set; }
    }

    public enum DatabaseType
    {
        /// <summary>
        /// Install >> Microsoft.Data.SqlClient
        /// </summary>
        SqlServer,
        /// <summary>
        /// Install >> MySqlConnector
        /// </summary>
        MySql,
        /// <summary>
        /// Install >> Microsoft.Data.Sqlite
        /// </summary>
        Sqlite
    }
}
