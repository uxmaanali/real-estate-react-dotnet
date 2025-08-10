namespace RealEstate.Utils;

public static class AppSettingExtension
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        var envName = environment.EnvironmentName;

        var fileName = Path.Combine(environment.ContentRootPath, $"appsettings.{envName}.json");

        configuration
            .AddJsonFile(Path.Combine(environment.ContentRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
            .AddJsonFile(Path.Combine(environment.ContentRootPath, $"appsettings.{envName}.json"), optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
        return services;
    }
}
