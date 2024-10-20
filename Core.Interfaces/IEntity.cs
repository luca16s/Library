// -----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;
public interface IEntity : IVersion
{
    public bool IsDeleted { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }
}

public interface IEntity<TId> : IEntity
{
    public TId Id { get; set; }
}