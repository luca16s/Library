// -----------------------------------------------------------------------
// <copyright file="IAggregate.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;
public interface IAggregate : IVersion
{ }

public interface IAggregate<TId> : IAggregate
{
    TId Id { get; set; }
    bool IsDeleted { get; set; }
    long? CreatedBy { get; set; }
    DateTime? CreatedAt { get; set; }
    long? LastModifiedBy { get; set; }
    DateTime? LastModified { get; set; }
}
