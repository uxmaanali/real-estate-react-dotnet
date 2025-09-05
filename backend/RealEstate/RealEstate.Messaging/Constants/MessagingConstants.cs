namespace RealEstate.Messaging.Constants;

using RealEstate.Messaging.Models;
using RealEstate.Shared.Utils;

public sealed class MessagingConstants
{
    public const string FanoutExchange = "fanout";
    public static string SendWelcomeMessageName = $"{nameof(SendWelcomeMessageEvent)}-{FanoutExchange.CapitalizeFirstLetter()}";
}
