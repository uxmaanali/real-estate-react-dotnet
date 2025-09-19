namespace RealEstate.MessageBroker.Consumers;
using System;
using System.Threading.Tasks;

using RealEstate.MessageBroker.Connection;
using RealEstate.MessageBroker.Events;

public class SendWelcomeTextConsumer : BaseConsumer<SendWelcomeMessageEvent>
{
    protected override string QueueName => $"{nameof(SendWelcomeMessageEvent)}.text";

    public SendWelcomeTextConsumer(IRabbitMqConnection rabbitMqConnection)
        : base(rabbitMqConnection)
    {
    }

    protected override async Task HandleMessageAsync(SendWelcomeMessageEvent message, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Text sent to {message.Mobile}");
        await Task.CompletedTask;
    }
}
