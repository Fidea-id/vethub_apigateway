using Application;
using Application.Utils;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console()
);

// Add services to the container.
services.AddApplicationServices(builder.Configuration);

// Add authentication services here if not already configured
services.AddAuthorization(options =>
{
    // Define your roles and corresponding policies here
    options.AddPolicy("RequireSuperadminRole", policy => policy.RequireRole("Superadmin"));
    options.AddPolicy("RequireOwnerRole", policy => policy.RequireRole("Owner"));
    // Add more policies for different roles if needed
});

services.AddControllers(options =>
{
    // Apply global authorization filter to enforce authorization on all actions
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser() // Require the user to be authenticated
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vethub API", Version = "v1" });

    // Include security requirements for JWT (or any other authentication mechanism)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Name = "Authorization",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// Register HttpClient as a singleton in your startup.cs
services.AddSingleton<HttpClient>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins("https://app.vethub.id")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
});

var app = builder.Build();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Vethub Dasboards",
    Authorization = new[] {
        new HangfireCustomBasicAuthenticationFilter {
            User = "vethub",
            Pass = "123.Qwer"
        }
    }
});
app.MapHangfireDashboard();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCustomExceptionHandler();
//app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "https://app.vethub.id");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    }
});

app.UseHttpsRedirection();


app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
