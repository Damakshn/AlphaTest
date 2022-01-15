using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AlphaTest.Infrastructure.Plugins;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Application;
using Microsoft.Extensions.Configuration;
using AlphaTest.WebApi.AccessControl;

namespace AlphaTest.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework(_configuration);
            services.AddConfiguredUserManagement();
            services.AddJwtAuth(_configuration);
            services.AddAccessControlRules();
            services.AddUtilityServices();
            services.AddTimeResolver(_configuration);
            services.AddApplicationLayer();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            SeedData.EnsurePopulated(app.ApplicationServices);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
