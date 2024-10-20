// -----------------------------------------------------------------------
// <copyright file="IVersion.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;

/// <summary>
/// Interface para lidar com concorrência.
/// </summary>
public interface IVersion
{
    long Version { get; set; }
}
