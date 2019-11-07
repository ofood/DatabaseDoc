using Microsoft.Extensions.Options;
using System;
using System.Data.Common;

namespace DatabaseDoc.Core.DbProvider
{
    public class DbManager : IDbManager
    {
        private readonly DbOptions _options;
        public DbManager(IOptionsMonitor<DbOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.CurrentValue;
        }

        public DbConnection CreateConnection(string name)
        {
            var dbSeting = GetSetting(name);

            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(dbSeting);
            DbConnection connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = dbSeting.ConnectionString;

            return connection;
        }

        private DbSetting GetSetting(string name)
        {
            _options.TryGetValue(name, out DbSetting dbSetting);

            return dbSetting;
        }
    }
}
