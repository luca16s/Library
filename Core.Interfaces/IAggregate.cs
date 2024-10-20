// -----------------------------------------------------------------------
// <copyright file="IAggregate.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;
public interface IAggregate : IEntity
{ }

public interface IAggregate<TId> : IAggregate, IEntity<TId>
{ }
