namespace RealEstate.Utils;

using RealEstate.Shared.OptionsConfig;
using RealEstate.Shared.OptionsConfig.Jwt;
using RealEstate.Shared.OptionsConfig.RabbitMq;

public static class OptionsSetup
{
    public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(OptionConstants.JwtSectionName))
            .ValidateDataAnnotations();

        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(OptionConstants.RabbitMqSectionName))
            .ValidateDataAnnotations();

        return services;
    }
}
