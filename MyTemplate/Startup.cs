using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using MyTemplate.DbContexts.SqliteDbContext;
using MyTemplate.DbContexts.MsSqlDbContext;
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
                loggingBuilder.AddNLog();
            });
            services.Configure<AppOptions>(ctx.Configuration.GetSection("WorkingDirectory"));
            
            services.AddTransient<MainController>();
            //services.AddSqlite<MySqliteDbContext>(connectionString: ctx.Configuration.GetSection("SqliteConnectionString").Value);
            services.AddDbContext<MySqliteDbContext>(options =>
                options.UseSqlite(ctx.Configuration.GetSection("SqliteConnectionString").Value));
            services.AddDbContext<MyMsSqlDbContext>(options => 
                options.UseSqlServer(ctx.Configuration.GetSection("MsSqlConnectionString").Value));

        services.AddTransient<SupportServicesPackage>();
        })
        .UseConsoleLifetime();
        
        AppHost = HostBuilder.Build();

    }

    public IServiceScope CreateScope()
    {
        return AppHost.Services.CreateScope();
    }
}
