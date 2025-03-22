using ContactPersistence.Data.Repositories;
using ContactPersistence.Events;
using RabbitMQ.Client;

namespace ContactPersistence.Consumers;

internal sealed class ContactDeletedConsumer : RabbitMqConsumerBase<ContactDeletedEvent>
{
    public ContactDeletedConsumer(IServiceProvider serviceProvider, ILogger<ContactDeletedConsumer> logger, IModel channel)
        : base(serviceProvider, logger, channel, "contact-deleted")
    {
    }

    protected override async Task HandleMessageAsync(ContactDeletedEvent contactEvent)
    {
        using var scope = _serviceProvider.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IContactRepository>();

        var contactId = contactEvent.ContactId;

        var contact = await repository.GetByIdAsync(contactId);

        if (contact is null)
        {
            throw new ArgumentNullException(nameof(contact));
        }

        await repository.DeleteAsync(contact);

        _logger.LogInformation($"Contact successfully deleted from database - Id: {contactId}");
    }
}
