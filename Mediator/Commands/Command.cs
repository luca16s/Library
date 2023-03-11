// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Commands
{
    using Mediator.Events;

    /// <summary>
    /// Classe base de comando sem retorno.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public abstract class Command<TId> : Message<TId> where TId : struct { }
}
