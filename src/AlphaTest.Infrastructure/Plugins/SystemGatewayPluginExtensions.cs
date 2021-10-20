using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using AlphaTest.Application;
using AlphaTest.Infrastructure.Mediation;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class SystemGatewayPluginExtensions
    {
        public static void AddSystemGateway(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ISystemGateway));
            services.AddScoped<ISystemGateway, AlphaTestSystemGateway>();
        }
    }
}
