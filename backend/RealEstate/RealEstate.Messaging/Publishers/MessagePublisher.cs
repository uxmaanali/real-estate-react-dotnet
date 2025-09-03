using MassTransit;

using RealEstate.Shared.Abstraction;

namespace RealEstate.Messaging.Publishers;

public class MessagePublisher : IMessagePublisher, IScopedDependency
{
    private readonly IPublishEndpoint _publisher;

    public MessagePublisher(IPublishEndpoint publisher)
    {
        _publisher = publisher;
    }

    public async Task Publish<T>(T message)
    {
        if (message is null)
            throw new ArgumentNullException("Cannot send empty message.");

        await _publisher.Publish(message);
    }
}
