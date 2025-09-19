namespace RealEstate.MessageBroker.Utils;

using Microsoft.Extensions.DependencyInjection;

using RealEstate.MessageBroker.Abstraction;
using RealEstate.MessageBroker.Connection;
using RealEstate.MessageBroker.Publisher;

public static class SetupRabbitMq
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        AddConsumers(services);

        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        services.AddSingleton<IMessagePublisher, MessagePublisher>();

        services.AddHostedService<RabbitMqListenerService>();

        return services;
    }

    private static void AddConsumers(IServiceCollection services)
    {
        var assembly = typeof(SetupRabbitMq).Assembly;

        var consumers = assembly.GetTypes()
            .Where(t => t.IsClass
                     && !t.IsAbstract
                     && typeof(IMessageConsumer).IsAssignableFrom(t));

        foreach (var consumer in consumers)
        {
            services.AddSingleton(typeof(IMessageConsumer), consumer);
        }
    }
}
