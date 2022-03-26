global using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;
//using NLog;

using MyTemplate.Controllers;

namespace MyTemplate;
public class Program
{
    public static void Main(string[] args)
    {
        //var logger = LogManager.GetCurrentClassLogger();

        WriteLine("Hello, World!");

        var startup = new Startup(args, "Development");


        using var scope = startup.CreateScope();
        var controller = scope.ServiceProvider.GetRequiredService<MainController>();

        var controller2 = scope.ServiceProvider.GetRequiredService<MainController>();
        controller2.ChangeDir();

        controller.LogDir();
        controller.ReadWriteSqliteMessage();

        
    }

}
