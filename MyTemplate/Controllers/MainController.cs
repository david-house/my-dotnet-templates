using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyTemplate.DbContexts.SqliteDbContext;
using MyTemplate.DbContexts.MsSqlDbContext;
using MyTemplate.Models;
using MyTemplate.DbContexts.Models;

namespace MyTemplate.Controllers;

public class MainController
{
    private ILogger<MainController> Logger { get; set; }
    private AppOptions AppOptions { get; set; }

    private IHostEnvironment HostEnvironment { get; set; }

    private IConfiguration Configuration { get; set; }

    private MySqliteDbContext SqliteDbContext { get; set; }
    private MyMsSqlDbContext MsSqlDbContext { get; set; }

    public MainController(ILogger<MainController> logger, IOptionsSnapshot<AppOptions> appOptions, 
        IHostEnvironment hostEnvironment, IConfiguration configuration, 
        MySqliteDbContext sqliteDbContext, MyMsSqlDbContext msSqlDbContext)
    {
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
        
    }

    public void LogDir()
    {
        Logger.LogInformation(AppOptions.WorkingDirectory);
    }

    public void ChangeDir()
    {
        AppOptions.WorkingDirectory = "none";
    }

    public void ReadWriteSqliteMessage()
    {
        var demoMessage = new DemoMessage() { Message = "Hello, World!", ModifiedDate = DateTimeOffset.Now };
        var result = SqliteDbContext.Add(demoMessage);
        SqliteDbContext.SaveChanges();
        Logger.LogInformation("Newly created Id {0}", result.Entity.DemoMessageId);


    }
}

