namespace RealEstate.MessageBroker.Consumers;

using System;
using System.Threading.Tasks;

using RealEstate.MessageBroker.Connection;
using RealEstate.MessageBroker.Events;

public class SendWelcomeEmailConsumer : BaseConsumer<SendWelcomeMessageEvent>
{
    protected override string QueueName => $"{nameof(SendWelcomeMessageEvent)}.email";

    public SendWelcomeEmailConsumer(IRabbitMqConnection rabbitMqConnection)
        : base(rabbitMqConnection)
    {
    }

    protected override async Task HandleMessageAsync(SendWelcomeMessageEvent message, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Email sent to {message.Email}");
        await Task.CompletedTask;
    }
}
