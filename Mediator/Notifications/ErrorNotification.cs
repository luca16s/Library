namespace Mediator.Notifications;
public class ErrorNotification(
    string id,
    string value
) : Notification(id, value)
{
    public string Exception { get; set; } = string.Empty;
    public string StackTrace { get; set; } = string.Empty;
}
