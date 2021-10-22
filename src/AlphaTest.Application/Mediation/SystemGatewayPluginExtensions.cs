using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace AlphaTest.Application.Mediation
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
