namespace RealEstate.Messaging.Consumers;
using System;
using System.Threading.Tasks;

using MassTransit;

using RealEstate.Messaging.Constants;
using RealEstate.Messaging.Models;

public class SendWelcomeMessageConsumer : IConsumer<SendWelcomeMessageEvent>
{
    public async Task Consume(ConsumeContext<SendWelcomeMessageEvent> context)
    {
        Console.WriteLine("Welcome message sent.");

        await Task.CompletedTask;
    }
}

public class SendWelcomMessageConsumerDefinition : ConsumerDefinition<SendWelcomeMessageConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<SendWelcomeMessageConsumer> consumerConfigurator, IRegistrationContext context)
    {
        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqConfigurator)
        {
            // Bind to the fanout exchange
            rabbitMqConfigurator.Bind(MessagingConstants.SendWelcomeMessageName);
        }
    }
}
