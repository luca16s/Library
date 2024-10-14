namespace Core.Events;

using Google.Protobuf;

using System.Collections.Generic;

public class MessageEnvelope(
    object? message,
    IDictionary<string, object?>? headers = null
)
{
    public object? Message { get; init; } = message;
    public IDictionary<string, object?> Headers { get; init; } = headers ?? new Dictionary<string, object?>();
}

public class MessageEnvelope<TMessage>(
    TMessage message,
    IDictionary<string, object?> header
) : MessageEnvelope(message, header)
    where TMessage : class, IMessage
{
    public new TMessage? Message { get; } = message;
}
