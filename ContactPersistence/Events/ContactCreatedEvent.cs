namespace ContactPersistence.Events;

internal sealed record ContactCreatedEvent(
    string Name,
    int DDDCode,
    string Phone,
    string? Email) : IntegrationEvent;
