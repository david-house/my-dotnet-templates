using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using MyTemplate.DbContexts.SqliteDbContext;
using MyTemplate.DbContexts.MsSqlDbContext;
using MyTemplate.Services;
//using NLog;

using MyTemplate.Controllers;
using MyTemplate.Models;
using Microsoft.Extensions.Configuration;

namespace MyTemplate;

public class Startup
{
    public IHostBuilder HostBuilder { get; }
    public IHost AppHost { get; }

    public Startup(string[] args, string environmentName)
    {
        HostBuilder = Host.CreateDefaultBuilder(args);
        HostBuilder.UseEnvironment(environmentName);

        HostBuilder.ConfigureAppConfiguration((ctx, config) =>
        {
            config.AddJsonFile("Configuration\\sample.json");
            
        });
        
        HostBuilder.ConfigureServices((ctx, services) =>
        {
            services.AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                loggingBuilder.AddNLog();
            });
            services.Configure<AppOptions>(ctx.Configuration.GetSection("WorkingDirectory"));
            
            services.AddTransient<DemoController>();
            
            services.AddDbContext<MySqliteDbContext>(options =>
                options.UseSqlite(ctx.Configuration.GetSection("SqliteConnectionString").Value));

            services.AddDbContext<MyMsSqlDbContext>(options => 
                options.UseSqlServer(ctx.Configuration.GetSection("MsSqlConnectionString").Value));

            services.AddTransient<IRuntimeEnvironment, MsWindowsRuntimeEnvironment>();
            services.AddHttpClient();
        })
        .UseConsoleLifetime();
        
        AppHost = HostBuilder.Build();

    }

    public IServiceScope CreateScope()
    {
        return AppHost.Services.CreateScope();
    }
}
