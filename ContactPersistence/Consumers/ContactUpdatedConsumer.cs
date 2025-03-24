using ContactPersistence.Data.Repositories;
using ContactPersistence.Events;
using RabbitMQ.Client;

namespace ContactPersistence.Consumers;

internal sealed class ContactUpdatedConsumer : RabbitMqConsumerBase<ContactUpdatedEvent>
{
    public ContactUpdatedConsumer(IServiceProvider serviceProvider, ILogger<ContactUpdatedConsumer> logger, IModel channel)
        : base(serviceProvider, logger, channel, "contact-updated")
    {
    }

    protected override async Task HandleMessageAsync(ContactUpdatedEvent contactEvent)
    {
        using var scope = _serviceProvider.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IContactRepository>();

        var contactId = contactEvent.ContactId;

        var existingContact = await repository.GetByIdAsync(contactId);

        if (existingContact is null)
        {
            throw new ArgumentNullException(nameof(existingContact));
        }

        existingContact.UpdateName(contactEvent.Name);
        existingContact.UpdateRegion(contactEvent.DDDCode);
        existingContact.UpdatePhone(contactEvent.Phone);

        if (!string.IsNullOrWhiteSpace(contactEvent.Email))
        {
            existingContact.UpdateEmail(contactEvent.Email);
        }

        await repository.UpdateAsync(existingContact);

        _logger.LogInformation($"Contact successfully updated to database - Id: {contactId}");
    }
}
