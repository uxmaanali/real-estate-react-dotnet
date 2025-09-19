namespace RealEstate.MessageBroker.Events;

using RealEstate.MessageBroker.Abstraction;

public record SendWelcomeMessageEvent(int UserId, string Email, string Mobile) : IFanoutMessage;
