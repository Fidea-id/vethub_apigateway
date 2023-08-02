using Application;
using Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
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
services.AddSwaggerGen();

// Register HttpClient as a singleton in your startup.cs
services.AddSingleton<HttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCustomExceptionHandler();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
