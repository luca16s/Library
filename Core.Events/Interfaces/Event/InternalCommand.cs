namespace Core.Events.Interfaces.Event;

using Core.Events.Interfaces.Command;

public record InternalCommand : IInternalCommand, ICommand;
