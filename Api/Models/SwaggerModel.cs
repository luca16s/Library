// -----------------------------------------------------------------------
// <copyright file="SwaggerInformation.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

/// <summary>
/// Informações para preenchimento do swagger.
/// </summary>
public class SwaggerInfo
{
    /// <summary>
    /// Site da aplicação.
    /// </summary>
    public string Site { get; set; } = string.Empty;

    /// <summary>
    /// E-Mail para contato com responsável pela aplicação.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Nome da aplicação.
    /// </summary>
    public string AppName { get; set; } = string.Empty;

    /// <summary>
    /// Versão da aplicação.
    /// </summary>
    public string AppVersion { get; set; } = string.Empty;

    /// <summary>
    /// Nome da companhia responsável pela aplicação.
    /// </summary>
    public string Company { get; set; } = string.Empty;

    /// <summary>
    /// Descrição da aplicação.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
