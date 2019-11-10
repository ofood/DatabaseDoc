using DatabaseDoc.Repository.Doc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDocRepostory, DocRepostory>();
            return services;
        }
    }
}
