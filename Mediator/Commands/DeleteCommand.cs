namespace ISO.SSY.Commands.Commands.Base;

using Mediator.Commands;

public class DeleteCommand : Command
{
    public DeleteCommand(long id) => Id = id;
}
