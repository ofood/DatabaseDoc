using DatabaseDoc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnitOfWorkExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            //services.AddSingleton(typeof(IUnitOfWork<>),typeof(UnitOfWork<>));
            //services.AddScoped<IDbContext, DapperDbContext>();
            services.AddSingleton<DapperDbContext>();
            services.AddSingleton<DapperUnitOfWorkFactory>();
            services.AddSingleton<EFUnitOfWorkFactory>();
            services.AddSingleton(factory =>
            {
                Func<string, IUnitOfWorkFactory> accesor = key =>
                  {
                      if (key.Equals(nameof(DapperUnitOfWorkFactory)))
                      {
                          return factory.GetService<DapperUnitOfWorkFactory>();
                      }
                      else if (key.Equals(nameof(EFUnitOfWorkFactory)))
                      {
                          return factory.GetService<EFUnitOfWorkFactory>();
                      }
                      else
                      {
                          throw new ArgumentException($"Not Support key : {key}");
                      }
                  };
                return accesor;
            });
            
            return services;
        }
    }
}
