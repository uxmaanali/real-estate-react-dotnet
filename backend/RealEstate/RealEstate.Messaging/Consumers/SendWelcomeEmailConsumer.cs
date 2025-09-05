namespace RealEstate.Messaging.Consumers;
using System.Threading.Tasks;

using MassTransit;

using RealEstate.Messaging.Constants;
using RealEstate.Messaging.Models;

public class SendWelcomeEmailConsumer : IConsumer<SendWelcomeMessageEvent>
{
    public async Task Consume(ConsumeContext<SendWelcomeMessageEvent> context)
    {
        Console.WriteLine("Welcome email sent");

        await Task.CompletedTask;
    }
}

public class SendWelcomeEmailConsumerDefinition : ConsumerDefinition<SendWelcomeEmailConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<SendWelcomeEmailConsumer> consumerConfigurator, IRegistrationContext context)
    {
        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqConfigurator)
        {
            // Configure the endpoint
            // rabbitMqConfigurator.ConcurrentMessageLimit = 10;
            // rabbitMqConfigurator.PrefetchCount = 20;

            // Bind to the fanout exchange
            rabbitMqConfigurator.Bind(MessagingConstants.SendWelcomeMessageName);
        }

        //    consumerConfigurator.UseMessageRetry(r => r
        //        .Interval(5, TimeSpan.FromSeconds(5))
        //        .Ignore<ArgumentException>());
    }
}

