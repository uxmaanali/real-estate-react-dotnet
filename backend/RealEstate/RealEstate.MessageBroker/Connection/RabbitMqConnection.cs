namespace RealEstate.MessageBroker.Connection;

using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using RabbitMQ.Client;

using RealEstate.Shared.OptionsConfig.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection, IAsyncDisposable
{
    private readonly RabbitMqOptions _options;
    private readonly SemaphoreSlim _connectionLock = new(1, 1);
    private IConnection _connection;
    private bool _disposed;

    public RabbitMqConnection(IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _options = rabbitMqOptions.Value;
    }

    public async Task<IConnection> GetConnection()
    {
        if (_disposed)
            throw new ObjectDisposedException($"Rabbit MQ {nameof(RabbitMqConnection)} is disposed");

        if (_connection?.IsOpen == true)
            return _connection;

        await _connectionLock.WaitAsync();
        try
        {
            if (_connection?.IsOpen == true)
                return _connection;

            return await CreateConnection();
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;

        await _connectionLock.WaitAsync();
        try
        {
            if (_connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }

            _disposed = true;
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    private async Task<IConnection> CreateConnection()
    {
        Console.WriteLine($"Rabbit MQ host {_options.HostName}");
        Console.WriteLine($"Rabbit MQ port {_options.Port}");
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            UserName = _options.Username,
            Password = _options.Password,
            Port = _options.Port ?? AmqpTcpEndpoint.UseDefaultPort,
            VirtualHost = _options.VirtualHost ?? "/",
        };

        var connection = await factory.CreateConnectionAsync();

        return connection;
    }
}
