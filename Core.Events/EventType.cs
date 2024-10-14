namespace Core.Events;
using System;

[Flags]
public enum EventType
{
    DomainEvent = 1,
    IntegrationEvent = 2,
    InternalCommand = 4
}
