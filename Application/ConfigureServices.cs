using Application.Services.Contracts;
using Application.Services.Implementations;
using Application.Utils;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRestAPIService, RestAPIService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = JwtUtil.Audience,
                    ValidIssuer = JwtUtil.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtUtil.Key))
                };
            });

            services.AddSingleton<IUriService>(provider =>
            {
                var uri = new BaseURI();
                var keyEnvironment = configuration["MyAppSettings:Environment"];
                if (keyEnvironment == "LOCAL")
                {
                    //LOCAL
                    uri.MasterAPIURI = "https://localhost:44382/api/";
                    uri.ClientAPIURI = "https://localhost:44381/api/";
                }
                else if (keyEnvironment == "PRODUCTION")
                {
                    //PRODUCTION
                    uri.MasterAPIURI = "https://master.fideaweb.my.id/api/";
                    uri.ClientAPIURI = "https://client.fideaweb.my.id/api/";
                }
                else if (keyEnvironment == "STAGING")
                {
                    //STAGING
                    uri.MasterAPIURI = "https://masterstaging.fideaweb.my.id/api/";
                    uri.ClientAPIURI = "https://clientstaging.fideaweb.my.id/api/";
                }
                else
                {
                    throw new Exception("Environment invalid");
                }
                return new UriService(uri);
            });

            return services;
        }
    }
}
