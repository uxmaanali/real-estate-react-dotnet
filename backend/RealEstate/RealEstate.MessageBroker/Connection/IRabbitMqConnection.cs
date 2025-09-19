namespace RealEstate.MessageBroker.Connection;

using RabbitMQ.Client;

public interface IRabbitMqConnection
{
    Task<IConnection> GetConnection();
}
