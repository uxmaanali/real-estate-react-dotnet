namespace RealEstate.MessageBroker.Utils;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using RealEstate.MessageBroker.Abstraction;

public class RabbitMqListenerService : BackgroundService
{
    private readonly IEnumerable<IMessageConsumer> _consumers;

    public RabbitMqListenerService(IEnumerable<IMessageConsumer> consumers)
    {
        _consumers = consumers;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        foreach (var consumer in _consumers)
        {
            await consumer.SubscribeAsync(cancellationToken);
        }

        await Task.Delay(Timeout.Infinite, cancellationToken);
    }
}
