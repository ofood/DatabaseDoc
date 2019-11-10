using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseDoc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using DatabaseDoc.Core.DbProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace DatabaseDoc.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //Log.Logger = new LoggerConfiguration()
            //   .ReadFrom.Configuration(configuration)
            //   .CreateLogger();
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
            services.AddHttpContextAccessor();

            services.AddDbManager(options =>
            {
                Configuration.Bind("DbOptions", options);
            });

            // https://www.cnblogs.com/dudu/p/10398225.html
            services.AddDbContextPool<EFDbContext>(options =>
            {
                options.UseSqlServer(Configuration["DbOptions:ItemBank:ConnectionString"]);
            }, 64);
            services.AddUnitOfWork();
            //添加仓储服务
            services.AddRepositories();
            //添加领域服务
            services.AddDomainServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                          name: "default",
                          template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
