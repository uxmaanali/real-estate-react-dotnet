namespace RealEstate.MessageBroker.Utils;
using System.Threading.Tasks;

using RabbitMQ.Client;

using RealEstate.MessageBroker.Constants;

internal static class DeclareQueues
{
    public static async Task DeclareTopicExchange(IChannel channel)
    {
        await channel.ExchangeDeclareAsync(
            exchange: QueueConstants.TopicExchange,
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false
        );
    }

    public static async Task DeclareFanoutExchange(IChannel channel)
    {
        await channel.ExchangeDeclareAsync(
            exchange: QueueConstants.FanoutExchange,
            type: ExchangeType.Fanout,
            durable: true,
            autoDelete: false
        );
    }
}
