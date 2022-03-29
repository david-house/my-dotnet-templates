namespace MyTemplate.Services;

public class MsWindowsRuntimeEnvironment : IRuntimeEnvironment
{
    public string GetCurrentUser() => $"{Environment.UserDomainName}\\{Environment.UserName}";
    public string GetDotNetCoreVersion() => Environment.Version.ToString();
    public string GetHostName() => Environment.MachineName;
}

public interface IRuntimeEnvironment
{
    string GetCurrentUser();
    string GetDotNetCoreVersion();
    string GetHostName();
    
}

