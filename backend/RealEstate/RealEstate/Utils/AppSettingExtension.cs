namespace RealEstate.Utils;

public static class AppSettingExtension
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        var envName = IsRunningInContainer() ? "Docker" : environment.EnvironmentName;

        configuration
            .AddJsonFile(Path.Combine(environment.ContentRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
            .AddJsonFile(Path.Combine(environment.ContentRootPath, $"appsettings.{envName}.json"), optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return services;
    }

    private static bool IsRunningInContainer()
    {
        return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true" ||
               Environment.GetEnvironmentVariable("CONTAINER") == "true" ||
               File.Exists("/.dockerenv");
    }
}
