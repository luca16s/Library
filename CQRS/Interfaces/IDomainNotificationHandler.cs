namespace CQRS.Interfaces
{
    using CQRS.Notifications;

    using MediatR;

    public interface IDomainNotificationHandler<TId, TResponse> : INotificationHandler<DomainNotification<TId, TResponse>>
        where TId : struct
        where TResponse : notnull
    {
        bool HasNotifications();

        void ClearNotifications();

        List<DomainNotification<TId, TResponse>> GetNotifications();
    }
}
