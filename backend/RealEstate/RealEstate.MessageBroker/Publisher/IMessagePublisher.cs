namespace RealEstate.MessageBroker.Publisher;

using System.Threading.Tasks;

public interface IMessagePublisher
{
    Task Publish<T>(T message, string? routingKey = null);

    Task PublishToQueue<T>(string queueName, T message, string? routingKey = null);
}
