using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RealEstate.Shared.Constants;

namespace RealEstate.Messaging.Utils;
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
                config.Host(connectionString, host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });
                config.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
