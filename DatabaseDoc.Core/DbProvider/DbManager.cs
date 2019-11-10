using Microsoft.Extensions.Options;
using System;
using System.Data.Common;

namespace DatabaseDoc.Core.DbProvider
{
    public class DbManager : IDbManager
    {
        public string NameConnectinString { get; set; } = "ItemBank";

        private readonly DbOptions _options;
        public DbManager(IOptionsMonitor<DbOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.CurrentValue;
        }

        public DbConnection CreateConnection()
        {
            var dbSeting = GetSetting();

            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(dbSeting);
            DbConnection connection = dbProviderFactory.CreateConnection();
            connection.ConnectionString = dbSeting.ConnectionString;

            return connection;
        }

        private DbSetting GetSetting()
        {
            _options.TryGetValue(NameConnectinString, out DbSetting dbSetting);

            return dbSetting;
        }
    }
}
