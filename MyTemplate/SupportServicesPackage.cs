
using Microsoft.Extensions.Hosting;
//using NLog;

using MyTemplate.Models;
using Microsoft.Extensions.Options;

namespace MyTemplate;

public class SupportServicesPackage
{
    public ILogger<SupportServicesPackage> Logger { get; set; }
    public IOptionsSnapshot<AppOptions> AppOptionsSnapshot { get; set; }
    public IHostEnvironment HostEnvironment { get; set; }
    public AppOptions AppOptions => AppOptionsSnapshot.Value;
    public SupportServicesPackage(ILogger<SupportServicesPackage> logger, IOptionsSnapshot<AppOptions> appOptions, IHostEnvironment hostEnvironment)
    {
        Logger = logger;
        AppOptionsSnapshot = appOptions;
        HostEnvironment = hostEnvironment;
        
    }
}