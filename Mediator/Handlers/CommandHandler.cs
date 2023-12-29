// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Handlers;

using Core.Models;

using FluentValidation.Results;

using Mediator.Interfaces;
using Mediator.Notifications;

/// <summary>
/// Classe de manipuladora de comandos com retorno.
/// </summary>
public abstract class CommandHandler
{
    protected readonly IMediatorHandler _mediator;
    protected readonly IDomainNotificationHandler _notifications;

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
        IDomainNotificationHandler notifications
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
    protected bool ValidateEntity(
        Entity entity
    )
    {
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
    protected void NotifyError(
        string nome,
        string mensagem
    )
    {
        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(mensagem))
        {
            return;
        }

        _ = _mediator.RaiseError(new ErrorNotification { StackTrace = mensagem, Exception = nome });
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
    protected void NotifyErrorValidations(
        ValidationResult validationResult
    )
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

/// <summary>
/// Classe de manipuladora de comandos com retorno.
/// </summary>
/// <typeparam name="TReturn">
/// Tipo do retorno.
/// </typeparam>
public abstract class CommandHandler<TReturn>
    where TReturn : notnull
{
    protected readonly IMediatorHandler mediator;
    protected readonly IDomainNotificationHandler notifications;

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
        IDomainNotificationHandler notifications
    )
    {
        this.mediator = mediator;
        this.notifications = notifications;
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
    protected bool ValidateEntity(
        Entity entity
    )
    {
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
    protected void NotifyError(
        string nome,
        string mensagem
    )
    {
        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(mensagem))
        {
            return;
        }

        _ = mediator.RaiseError(new ErrorNotification { StackTrace = mensagem, Exception = nome });
    }

    /// <summary>
    /// Indica se há notificações.
    /// </summary>
    /// <returns>
    /// Retorna se há notificações.
    /// True: caso existam notificações.
    /// </returns>
    protected bool HasNotifications() => notifications.HasNotifications();

    /// <summary>
    /// Notifica validações de erro.
    /// </summary>
    /// <param name="validationResult">
    /// Validação a ser notificada.
    /// </param>
    protected void NotifyErrorValidations(
        ValidationResult validationResult
    )
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
