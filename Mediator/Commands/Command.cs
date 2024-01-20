// -----------------------------------------------------------------------
// <copyright file="Command.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Commands;

using MediatR;

/// <summary>
/// Classe base de comando sem retorno.
/// </summary>
public abstract class Command : BaseCommand, IRequest
{ }
