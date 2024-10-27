// -----------------------------------------------------------------------
// <copyright file="Swagger.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Api.Models;
/// <summary>
/// Informações para preenchimento do swagger.
/// </summary>
public class Swagger
{
    /// <summary>
    /// Versão da aplicação.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Versão do Swagger.
    /// </summary>
    public long[] SwaggerVersions { get; set; } = [];

    /// <summary>
    /// Descrição da aplicação.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Indica como é formatado o Token.
    /// </summary>
    public string BearerFormat { get; set; } = string.Empty;

    /// <summary>
    /// Nome da autorização HTTP.
    /// </summary>
    public string OAuthScheme { get; set; } = string.Empty;

    /// <summary>
    /// Nome do header de segurança.
    /// </summary>
    public string SecuritySchemeHeaderName { get; set; } = string.Empty;

    /// <summary>
    /// Descrição curta do esquema de segurança.
    /// </summary>
    public string SecuritySchemeDescription { get; set; } = string.Empty;
}
