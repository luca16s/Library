// -----------------------------------------------------------------------
// <copyright file="DeleteCommand.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Commands;
public class DeleteCommand : Command
{
    public DeleteCommand(long id) => Id = id;
}
