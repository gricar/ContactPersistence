using ContactPersistence.Data.Repositories;
using ContactPersistence.Events;
using ContactPersistence.Models;
using RabbitMQ.Client;

namespace ContactPersistence.Consumers;

internal sealed class ContactCreatedConsumer : RabbitMqConsumerBase<ContactCreatedEvent>
{
    public ContactCreatedConsumer(IServiceProvider serviceProvider, ILogger<ContactCreatedConsumer> logger, IModel channel)
        : base(serviceProvider, logger, channel, "contact-created")
    {
    }

    protected override async Task HandleMessageAsync(ContactCreatedEvent contactEvent)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IContactRepository>();
        var contact = new Contact()
        {
            Name = contactEvent.Name,
            DddCode = contactEvent.DDDCode,
            Phone = contactEvent.Phone,
            Email = contactEvent.Email,
        };

        await repository.AddAsync(contact);

        _logger.LogInformation($"Contact successfully saved to database - Id: {contact.Id}");
    }
}