using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AlphaTest.Application;
using AlphaTest.Application.UtilityServices.API;
using AlphaTest.Infrastructure.Plugins;
using AlphaTest.Infrastructure.Database;
using AlphaTest.WebApi.AccessControl;
using AlphaTest.WebApi.Utils;

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
            services.AddScoped<IUrlGenerator, UrlGenerator>();
            services.AddTimeResolver(_configuration);
            services.AddBackgroundJobs();
            services.AddApplicationLayer();
            services.AddControllers();
        }
        
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
