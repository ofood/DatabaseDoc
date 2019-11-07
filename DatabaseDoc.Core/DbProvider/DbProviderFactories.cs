using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Reflection;

namespace DatabaseDoc.Core.DbProvider
{
    public static class DbProviderFactories
    {
        private static ConcurrentDictionary<string, DbProviderFactory> _factories = new ConcurrentDictionary<string, DbProviderFactory>(StringComparer.OrdinalIgnoreCase);

        public static DbProviderFactory GetFactory(DbSetting dbSetting)
        {
            if (_factories.ContainsKey(dbSetting.DatabaseType.ToString()))
            {
                return _factories[dbSetting.DatabaseType.ToString()];
            }

            var factoryType = Type.GetType(dbSetting.AssemblyName);
            if (factoryType == null)
            {
                throw new ArgumentException($"{dbSetting.AssemblyName} can not load");
            }

            var providerInstance = factoryType.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static);

            if (providerInstance == null)
            {
                throw new InvalidOperationException(dbSetting.AssemblyName);
            }

            if (!providerInstance.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
            {
                throw new InvalidOperationException(dbSetting.AssemblyName);
            }

            var factory = providerInstance.GetValue(null) as DbProviderFactory;
            if (null == factory)
            {
                throw new InvalidOperationException(dbSetting.AssemblyName);
            }
            _factories.TryAdd(dbSetting.DatabaseType.ToString(), factory);

            return factory;
        }
    }
}

