namespace RealEstate.Messaging.Abstraction;
using MassTransit;

public interface IConfigurableEvent
{
    void Configure(IRabbitMqBusFactoryConfigurator config);
}
