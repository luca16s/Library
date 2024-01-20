// -----------------------------------------------------------------------
// <copyright file="ErrorNotification.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Notifications;

using MediatR;

/// <summary>
/// Classe de notificação de erro.
/// </summary>
public class ErrorNotification : INotification
{
    /// <summary>
    /// Exceção lançada pela aplicação.
    /// </summary>
    public required string Exception { get; set; }

    /// <summary>
    /// Pilha de erros lançada pelo sistema.
    /// </summary>
    public required string StackTrace { get; set; }
}
