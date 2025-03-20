using ContactPersistence.Events;
using RabbitMQ.Client;

namespace ContactPersistence.Consumers;

/*internal sealed class ContactUpdatedConsumer : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string _queueName = "contact-updated";
    private readonly ILogger<ContactUpdatedConsumer> _logger;

    public ContactUpdatedConsumer(IServiceProvider serviceProvider, ILogger<ContactUpdatedConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            ClientProvidedName = "ContactPersistencConsumer"
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_queueName, true, false, false, null);
    }

    protected override Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Update contact consumer");

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var contact = JsonSerializer.Deserialize<ContactUpdtedEvent>(message);

            _logger.LogInformation("Message received {contact}", contact);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(_queueName, true, consumer);

        return Task.CompletedTask;
    }
}*/

internal sealed class ContactUpdatedConsumer : RabbitMqConsumerBase<ContactUpdatedEvent>
{
    public ContactUpdatedConsumer(IServiceProvider serviceProvider, ILogger<ContactUpdatedConsumer> logger, IModel channel)
        : base(serviceProvider, logger, channel, "contact-updated")
    {
    }

    protected override Task HandleMessageAsync(ContactUpdatedEvent contact)
    {
        return Task.CompletedTask;
    }
}
