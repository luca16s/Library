// -----------------------------------------------------------------------
// <copyright file=".cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Handlers
{
    using Core.Models;

    using CQRS.Interfaces;
    using CQRS.Notifications;

    using FluentValidation.Results;

    using MediatR;

    using System.Threading.Tasks;

    /// <summary>
    /// Classe de manipuladora de comandos.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public abstract class CommandHandler<TId>
        where TId : struct
    {
        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationHandler<TId> _notifications;

        protected CommandHandler(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification<TId>> notifications
        )
        {
            _mediator = mediator;
            _notifications = (DomainNotificationHandler<TId>)notifications;
        }

        protected bool ValidateEntity(Entity<TId> entity)
        {
            if (entity.IsConsistent())
                return true;

            NotifyErrorValidations(entity.ValidationResult);
            return false;
        }

        protected void NotifyErrorValidations(ValidationResult validationResult)
        {
            foreach (ValidationFailure? error in validationResult.Errors)
            {
                if (error == null)
                {
                    continue;
                }

                NotifyError(error.PropertyName, error.ErrorMessage);
            }
        }

        protected void NotifyError(string nome, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(mensagem))
            {
                return;
            }

            _ = _mediator.RaiseEvent<DomainNotification<TId>, TId>(new DomainNotification<TId>(nome, mensagem));
        }

        protected bool HasNotifications() => _notifications.HasNotifications();

        protected static Task<bool> Sucess() => Task.FromResult(true);

        protected static Task<bool> Failed() => Task.FromResult(false);
    }
}
