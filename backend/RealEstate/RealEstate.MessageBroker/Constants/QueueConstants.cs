namespace RealEstate.MessageBroker.Constants;

public class QueueConstants
{
    public const string FanoutExchange = "real-estate-fanout-exchange"; // Ignore routing key and send message to all queues
    public const string TopicExchange = "real-estate-topic-exchange"; // Based on routing key
}
