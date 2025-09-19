namespace RealEstate.MessageBroker.Abstraction;

using System.Threading.Tasks;

public interface IMessageConsumer
{
    Task SubscribeAsync(CancellationToken cancellationToken);
}