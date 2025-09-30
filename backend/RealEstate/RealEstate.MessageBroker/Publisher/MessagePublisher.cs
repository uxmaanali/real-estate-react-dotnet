namespace RealEstate.MessageBroker.Publisher;

using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using RabbitMQ.Client;

using RealEstate.MessageBroker.Abstraction;
using RealEstate.MessageBroker.Connection;
using RealEstate.MessageBroker.Constants;
using RealEstate.MessageBroker.Utils;
using RealEstate.Shared.Extensions;

public class MessagePublisher : IMessagePublisher
{
    private readonly IRabbitMqConnection _rabbitMqConnection;

    public MessagePublisher(IRabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public async Task Publish<T>(T message, string? routingKey = null)
    {
        var connection = await _rabbitMqConnection.GetConnection();
        using var channel = await connection.CreateChannelAsync();

        var queueName = typeof(T).Name.ToKebabCase();
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        var props = new BasicProperties { DeliveryMode = DeliveryModes.Persistent };

        var isFanoutMessage = typeof(IFanoutMessage).IsAssignableFrom(typeof(T));
        var isTopicMessage = typeof(ITopicMessage).IsAssignableFrom(typeof(T));
        var isDirectMessage = typeof(IDirectMessage).IsAssignableFrom(typeof(T));

        string publishExchange;
        string publishRoutingKey;

        if (isFanoutMessage)
        {
            await DeclareQueues.DeclareFanoutExchange(channel);
            publishExchange = QueueConstants.FanoutExchange;
            publishRoutingKey = string.Empty; // ignored in fanout
        }
        else if (isTopicMessage)
        {
            if (string.IsNullOrEmpty(routingKey))
                throw new ArgumentNullException($"{nameof(routingKey)} is not defined.");

            await DeclareQueues.DeclareTopicExchange(channel);
            publishExchange = QueueConstants.TopicExchange;
            publishRoutingKey = routingKey!;
        }
        else if (isDirectMessage)
        {
            // Default: direct to queue
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
            publishExchange = string.Empty; // default exchange
            publishRoutingKey = queueName;
        }
        else
        {
            throw new Exception($"Message is not proper to send.");
        }

        await channel.BasicPublishAsync(
            exchange: publishExchange,
            routingKey: publishRoutingKey,
            mandatory: false,
            basicProperties: props,
            body: body
        );
    }

    public async Task PublishToQueue<T>(string queueName, T message, string? routingKey = null)
    {
        var connection = await _rabbitMqConnection.GetConnection();
        using var channel = await connection.CreateChannelAsync();

        queueName = queueName.ToKebabCase();
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        var props = new BasicProperties { DeliveryMode = DeliveryModes.Persistent };

        var isFanoutMessage = typeof(IFanoutMessage).IsAssignableFrom(typeof(T));
        var isTopicMessage = typeof(ITopicMessage).IsAssignableFrom(typeof(T));
        var isDirectMessage = typeof(IDirectMessage).IsAssignableFrom(typeof(T));

        string publishExchange;
        string publishRoutingKey;

        if (isFanoutMessage)
        {
            await DeclareQueues.DeclareFanoutExchange(channel);
            publishExchange = QueueConstants.FanoutExchange;
            publishRoutingKey = string.Empty; // ignored in fanout
        }
        else if (isTopicMessage)
        {
            if (string.IsNullOrEmpty(routingKey))
                throw new ArgumentNullException($"{nameof(routingKey)} is not defined.");

            await DeclareQueues.DeclareTopicExchange(channel);
            publishExchange = QueueConstants.TopicExchange;
            publishRoutingKey = routingKey!;
        }
        else if (isDirectMessage)
        {
            // Default: direct to queue
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
            publishExchange = string.Empty; // default exchange
            publishRoutingKey = queueName;
        }
        else
        {
            throw new Exception($"Message is not proper to send.");
        }

        await channel.BasicPublishAsync(
            exchange: publishExchange,
            routingKey: publishRoutingKey,
            mandatory: false,
            basicProperties: props,
            body: body
        );
    }
}
