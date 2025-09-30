namespace RealEstate.MessageBroker.Consumers;
using System;
using System.Text.Json;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using RealEstate.MessageBroker.Abstraction;
using RealEstate.MessageBroker.Connection;
using RealEstate.MessageBroker.Constants;
using RealEstate.MessageBroker.Utils;
using RealEstate.Shared.Extensions;

public abstract class BaseConsumer<T> : IMessageConsumer, IAsyncDisposable
{
    private readonly IRabbitMqConnection _rabbitMqConnection;
    private IChannel? _channel;
    private string? _queueName;

    protected BaseConsumer(IRabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    protected abstract string QueueName { get; }

    protected virtual string? GetRoutingKey()
    {
        return null;
    }

    /// <summary>
    /// Override this in derived consumer to handle messages.
    /// </summary>
    protected abstract Task HandleMessageAsync(T message, CancellationToken cancellationToken);

    public async Task SubscribeAsync(CancellationToken cancellationToken)
    {
        var connection = await _rabbitMqConnection.GetConnection();
        _channel = await connection.CreateChannelAsync();

        _queueName = QueueName.ToKebabCase();

        var isFanoutMessage = typeof(IFanoutMessage).IsAssignableFrom(typeof(T));
        var isTopicMessage = typeof(ITopicMessage).IsAssignableFrom(typeof(T));
        var isDirectMessage = typeof(IDirectMessage).IsAssignableFrom(typeof(T));

        var routingKey = GetRoutingKey(); // optional hook for subclasses

        if (isFanoutMessage)
        {
            // Events → fanout exchange
            await DeclareQueues.DeclareFanoutExchange(_channel);
            await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);
            await _channel.QueueBindAsync(_queueName, QueueConstants.FanoutExchange, string.Empty);
        }
        else if (isTopicMessage)
        {
            if (string.IsNullOrEmpty(routingKey))
                throw new ArgumentNullException($"{nameof(routingKey)} is not defined.");

            // Messages with routing key → topic exchange
            await DeclareQueues.DeclareTopicExchange(_channel);
            await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);
            await _channel.QueueBindAsync(_queueName, QueueConstants.TopicExchange, routingKey);
        }
        else if (isDirectMessage)
        {
            // Default: direct queue
            await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false);
        }
        else
        {
            throw new Exception($"Message is not proper to consume.");
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            try
            {
                var message = JsonSerializer.Deserialize<T>(args.Body.Span);
                if (message is not null)
                {
                    await HandleMessageAsync(message, cancellationToken);
                }

                await _channel.BasicAckAsync(args.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                // log ex
                await _channel.BasicNackAsync(args.DeliveryTag, multiple: false, requeue: true);
            }
        };

        // Start consuming
        await _channel.BasicConsumeAsync(
            queue: _queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken
        );

    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.DisposeAsync();
            _channel = null;
        }
    }
}
