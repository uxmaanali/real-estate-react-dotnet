namespace RealEstate.Messaging.Utils;
using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using RealEstate.Shared.Constants;
using RealEstate.Shared.OptionsConfig.RabbitMq;

public static class SetupMessaging
{

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStrings.RabbitMqConnection)
            ?? throw new InvalidOperationException($"Connection string '{ConnectionStrings.RabbitMqConnection}' not found.");

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            var assembly = typeof(SetupMessaging).Assembly;

            x.AddConsumers(assembly);
            x.AddSagaStateMachines(assembly);
            x.AddSagas(assembly);
            //x.AddActivities(assembly);

            x.UsingRabbitMq((context, config) =>
            {
                var rabbitMqConfig = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                config.Host(connectionString, host =>
                {
                    host.Username(rabbitMqConfig.Username);
                    host.Password(rabbitMqConfig.Password);
                });

                config.AddConfigurableEvents(assembly);

                config.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
