namespace Core.Events.Interfaces.Query;

using MediatR;

public interface IQuery<out T> : IRequest<T>
    where T : notnull
{ }
