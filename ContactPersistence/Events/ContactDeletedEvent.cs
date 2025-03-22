namespace ContactPersistence.Events;

internal sealed record ContactDeletedEvent(Guid ContactId) : IntegrationEvent;
