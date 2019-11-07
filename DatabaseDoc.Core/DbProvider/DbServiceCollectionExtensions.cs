using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DatabaseDoc.Core.DbProvider
{
    public static class DbServiceCollectionExtensions
    {
        public static IServiceCollection AddDbManager(this IServiceCollection services, Action<DbOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.TryAddSingleton<IDbManager, DbManager>();
            return services;
        }
    }
}
