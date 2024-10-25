// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Api.Models;

using Core.CrossCutting;

using System.Collections.Generic;

public class Settings
{
    /// <summary>
    /// Informações para construção do Token JWT.
    /// </summary>
    public Jwt Jwt { get; set; } = new();

    /// <summary>
    /// Informações para construção do swagger.
    /// </summary>
    public Swagger Swagger { get; set; } = new();

    /// <summary>
    /// Informações da aplicação.
    /// </summary>
    public ApplicationInfo AppInfo { get; set; } = new();

    /// <summary>
    /// Nome das regras de CORS a serem aplicadas.
    /// </summary>
    public string CorsPolicyName { get; set; } = string.Empty;

    /// <summary>
    /// Url de APIs a serem consumidas.
    /// </summary>
    public IEnumerable<Connection> ApiUrls { get; set; } = [];

    /// <summary>
    /// Domínios permitidos para acesso.
    /// </summary>
    public IEnumerable<string> AllowedDomains { get; set; } = [];

    /// <summary>
    /// Strings de conexão.
    /// </summary>
    public IEnumerable<Connection> ConnectionStrings { get; set; } = [];
}
