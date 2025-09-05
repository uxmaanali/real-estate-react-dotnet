namespace RealEstate.Messaging.Models;

using MassTransit;

using RealEstate.Messaging.Abstraction;
using RealEstate.Messaging.Constants;

public record SendWelcomeMessageEvent : IConfigurableEvent
{
    public int UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public void Configure(IRabbitMqBusFactoryConfigurator config)
    {
        config.Message<SendWelcomeMessageEvent>(x => x.SetEntityName(MessagingConstants.SendWelcomeMessageName));
        config.Publish<SendWelcomeMessageEvent>(x => x.ExchangeType = MessagingConstants.FanoutExchange);
    }
}
