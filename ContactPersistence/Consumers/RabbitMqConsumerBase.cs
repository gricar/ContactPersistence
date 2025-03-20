using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ContactPersistence.Consumers;

internal abstract class RabbitMqConsumerBase<TEvent> : BackgroundService
{
    protected readonly IServiceProvider _serviceProvider;
    private readonly IModel _channel;
    protected readonly ILogger _logger;
    private readonly string _queueName;

    protected RabbitMqConsumerBase(IServiceProvider serviceProvider, ILogger logger, IModel channel, string queueName)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _channel = channel;
        _queueName = queueName;

        _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation($"Starting consumer for queue: {_queueName}");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonSerializer.Deserialize<TEvent>(message);

            if (@event != null)
            {
                _logger.LogInformation("Message received on queue {QueueName}: {Message}", _queueName, message);
                await HandleMessageAsync(@event);
            }

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(_queueName, false, consumer);

        return Task.CompletedTask;
    }

    protected abstract Task HandleMessageAsync(TEvent @event);
}
