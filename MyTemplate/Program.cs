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
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.BackgroundColor = ConsoleColor.White;
        WriteLine("Begin startup");
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;

        var startup = new Startup(args, "Development");


        using var scope = startup.CreateScope();
        var controller = scope.ServiceProvider.GetRequiredService<DemoController>();

        var controller2 = scope.ServiceProvider.GetRequiredService<DemoController>();
        controller2.ChangeDir();

        controller.LogDir();
        controller.ReadWriteSqliteMessage();

        var theWeather = controller.ReadWeatherApi();

        
    }

}

