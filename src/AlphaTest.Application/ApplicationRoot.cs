using AlphaTest.Application.Mediation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AlphaTest.Application
{
    public static class ApplicationRoot
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationRoot)));
            services.AddMediatR(typeof(ISystemGateway));
            services.AddScoped<ISystemGateway, AlphaTestSystemGateway>();
        }
    }
}
