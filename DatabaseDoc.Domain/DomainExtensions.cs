using DatabaseDoc.Domain.Doc;
using DatabaseDoc.Domain.Doc.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IDocService, DocService>();

            return services;
        }
    }
}
