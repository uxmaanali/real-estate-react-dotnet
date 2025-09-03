namespace RealEstate.Messaging.Publishers;

public interface IMessagePublisher
{
    Task Publish<T>(T message);
}
