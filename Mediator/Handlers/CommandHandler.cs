// -----------------------------------------------------------------------
// <copyright file=".cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Handlers
{
    using Core.Models;

    using FluentValidation.Results;

    using Mediator.Interfaces;
    using Mediator.Notifications;

    /// <summary>
    /// Classe de manipuladora de comandos.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// Tipo do retorno.
    /// </typeparam>
    public abstract class CommandHandler<TId, TResponse>
        where TId : struct
        where TResponse : notnull
    {
        protected readonly IMediatorHandler _mediator;
        protected readonly IDomainNotificationHandler<TId, TResponse> _notifications;

        /// <summary>
        /// Construtor da classe de manipulação de comandos.
        /// </summary>
        /// <param name="mediator">
        /// Interface do mediator.
        /// </param>
        /// <param name="notifications">
        /// Inteface do notificador de domínio.
        /// </param>
        protected CommandHandler(
            IMediatorHandler mediator,
            IDomainNotificationHandler<TId, TResponse> notifications
        )
        {
            _mediator = mediator;
            _notifications = notifications;
        }

        /// <summary>
        /// Valida entidade de domínio passada.
        /// </summary>
        /// <param name="entity">
        /// Entidade a ser validada.
        /// </param>
        /// <returns>
        /// Retorna se entidade está válida.
        /// True: Caso válida.
        /// </returns>
        protected bool ValidateEntity(Entity<TId> entity)
        {
            if (entity.IsConsistent())
                return true;

            NotifyErrorValidations(entity.ValidationResult);
            return false;
        }

        /// <summary>
        /// Notifica eventos do comando.
        /// </summary>
        /// <param name="nome">
        /// Nome do evento a ser notificado.
        /// </param>
        /// <param name="mensagem">
        /// Mensagem da notificação.
        /// </param>
        protected void NotifyError(string nome, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(mensagem))
            {
                return;
            }

            _ = _mediator.RaiseEvent<DomainNotification<TId, TResponse>, TId, TResponse>(new DomainNotification<TId, TResponse>(nome, mensagem));
        }

        /// <summary>
        /// Indica se há notificações.
        /// </summary>
        /// <returns>
        /// Retorna se há notificações.
        /// True: caso existam notificações.
        /// </returns>
        protected bool HasNotifications() => _notifications.HasNotifications();

        /// <summary>
        /// Notifica validações de erro.
        /// </summary>
        /// <param name="validationResult">
        /// Validação a ser notificada.
        /// </param>
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
    }
}
