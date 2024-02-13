using Application.Services.Contracts;
using Application.Services.Implementations;
using Application.Utils;
using Domain.Entities;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System.Text;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRestAPIService, RestAPIService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IDocGenerateService, DocGenerateService>();

            var connectionString = "";

            var environment = configuration["MyAppSettings:Environment"];
            if (environment == "LOCAL")
            {
                // Local connection string
                connectionString = "server=localhost;database=VethubMaster;uid=root;Pooling=false;Allow User Variables=true;";
            }
            else if (environment == "STAGING")
            {
                // Staging connection string
                connectionString = "server=localhost;database=vethubmaster;uid=adminmaster;password=S@k2lu87COAlYOcNOfApuB1nu26o1a;Pooling=false;Allow User Variables=true;";
            }
            else
            {
                // Production connection string
                connectionString = "server=localhost;database=vethubmaster;uid=adminmaster;password=SAl@k2lu87CO6oNOfApuB1nu21YOca;Pooling=false;Allow User Variables=true;";
            }

            // Add Hangfire services.
            var hangFireWorkerCount = 3;
            var hangFireJobExpirationTimeOut = 30;
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
                {
                    TablesPrefix = "Hangfire_"
                }))
                .WithJobExpirationTimeout(TimeSpan.FromDays(hangFireJobExpirationTimeOut)));

            // Add the processing server as IHostedService
            services.AddHangfireServer(options => options.WorkerCount = hangFireWorkerCount);

            services.AddSingleton<RestClient>();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5); // Set server-side timeout
            });

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
                    uri.APIURI = "https://localhost:44380/api/";
                    uri.APIWEBURI = "https://localhost:44380/";
                }
                else if (keyEnvironment == "PRODUCTION")
                {
                    //PRODUCTION
                    uri.MasterAPIURI = "https://master.vethub.id/api/";
                    uri.ClientAPIURI = "https://client.vethub.id/api/";
                    uri.APIURI = "https://api.vethub.id/api/";
                    uri.APIWEBURI = "https://api.vethub.id/";
                }
                else if (keyEnvironment == "STAGING")
                {
                    //STAGING
                    uri.MasterAPIURI = "https://mastersg.vethub.id/api/";
                    uri.ClientAPIURI = "https://clientsg.vethub.id/api/";
                    uri.APIURI = "https://apisg.vethub.id/api/";
                    uri.APIWEBURI = "https://apisg.vethub.id/";
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
