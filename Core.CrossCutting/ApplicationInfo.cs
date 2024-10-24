// -----------------------------------------------------------------------
// <copyright file="ApplicationInfo.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.CrossCutting;

/// <summary>
/// Classe com informações sobre o projeto.
/// </summary>
public class ApplicationInfo
{
    /// <summary>
    /// Nome da aplicação.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nome da companhia responsável pela aplicação.
    /// </summary>
    public string Company { get; set; } = string.Empty;

    /// <summary>
    /// Site da aplicação.
    /// </summary>
    public string Site { get; set; } = string.Empty;

    /// <summary>
    /// E-Mail para contato com responsável pela aplicação.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
