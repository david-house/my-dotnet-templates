using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyTemplate.DbContexts.SqliteDbContext;
using MyTemplate.DbContexts.MsSqlDbContext;
using MyTemplate.Models;
using MyTemplate.DbContexts.Models;
using MyTemplate.Services;
using System.Net.Http.Json;

namespace MyTemplate.Controllers;

public class DemoController
{
    private ILogger<DemoController> Logger { get; set; }
    private AppOptions AppOptions { get; set; }

    private IHostEnvironment HostEnvironment { get; set; }

    private IConfiguration Configuration { get; set; }

    private MySqliteDbContext SqliteDbContext { get; set; }
    private MyMsSqlDbContext MsSqlDbContext { get; set; }
    private IRuntimeEnvironment RuntimeEnvironmentService { get; set; }
    private IHttpClientFactory HttpClientFactory { get; set; }

    public DemoController(ILogger<DemoController> logger, IOptionsSnapshot<AppOptions> appOptions, 
        IHostEnvironment hostEnvironment, IConfiguration configuration, 
        MySqliteDbContext sqliteDbContext, MyMsSqlDbContext msSqlDbContext,
        IRuntimeEnvironment runtimeEnvironmentService, IHttpClientFactory httpClientFactory)
    {
        HttpClientFactory = httpClientFactory;
        Configuration = configuration;
        HostEnvironment = hostEnvironment;
        AppOptions = appOptions.Value;
        Logger = logger;
        Logger.LogInformation("Hello World! log message");
        Logger.LogInformation("Working directory is {0}", AppOptions.WorkingDirectory);
        Logger.LogInformation("The environment is {0}", HostEnvironment.EnvironmentName);
        Logger.LogInformation(Configuration.GetSection("foo").Value);
        AppOptions.WorkingDirectory = Environment.CurrentDirectory;
        SqliteDbContext = sqliteDbContext;
        MsSqlDbContext = msSqlDbContext;
        RuntimeEnvironmentService = runtimeEnvironmentService;
        Logger.LogInformation("Executing as {0}", RuntimeEnvironmentService.GetCurrentUser());
        Logger.LogInformation(".Net Core Version {0}", RuntimeEnvironmentService.GetDotNetCoreVersion());
        Logger.LogInformation("Machinename {0}", RuntimeEnvironmentService.GetHostName());
    }

    public void LogDir()
    {
        Logger.LogInformation(AppOptions.WorkingDirectory);
    }

    public void ChangeDir()
    {
        AppOptions.WorkingDirectory = "none";
    }

    public WeatherApiPoints ReadWeatherApi()
    {
        using var httpClient = HttpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "(myweatherapp.com, contact@myweatherapp.com)");
        var result = httpClient.GetFromJsonAsync<WeatherApiPoints>(requestUri:"https://api.weather.gov/points/39.7456,-97.0892").Result;
        return result;
    }

    public void ReadWriteSqliteMessage()
    {
        var demoMessage = new DemoMessage() { Message = "Hello, World!", ModifiedDate = DateTimeOffset.Now };
        var result = SqliteDbContext.Add(demoMessage);
        SqliteDbContext.SaveChanges();
        Logger.LogInformation("Newly created Id {0}", result.Entity.DemoMessageId);


        var demoMessage2 = new DemoMessage() { Message = "Hello, World! 2", ModifiedDate = DateTimeOffset.Now };
        var result2 = MsSqlDbContext.Add(demoMessage2);
        MsSqlDbContext.SaveChanges();
        Logger.LogInformation("Newly created Id {0}", result2.Entity.DemoMessageId);


    }
}

