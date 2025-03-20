namespace ContactPersistence.Events;

internal sealed record ContactUpdatedEvent(
    Guid Id,
    string Name,
    int DDDCode,
    string Phone,
    string? Email);