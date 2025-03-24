namespace ContactPersistence.Events;

internal sealed record ContactUpdatedEvent(
    Guid ContactId,
    string Name,
    int DDDCode,
    string Phone,
    string? Email) : IntegrationEvent;