using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using AlphaTest.Infrastructure.Auth.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class JwtAuthPluginExtension
    {
        public static void AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ALPHATEST_TOKEN_KEY"]));
            services.AddScoped<JwtGenerator>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    // https://wildermuth.com/2018/04/10/Using-JwtBearer-Authentication-in-an-API-only-ASP-NET-Core-Project
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = signingKey
                    };
                });
        }
    }
}
