// -----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;
public interface IEntity
{ }

public interface IEntity<TId>
{
    public TId Id { get; set; }
}
