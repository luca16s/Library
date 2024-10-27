// -----------------------------------------------------------------------
// <copyright file="ApiUrl.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Api.Models;
using System.Collections.Generic;

public class ApiUrl
{
    /// <summary>
    /// Url de conexão podendo ser com Banco de Dados ou API.
    /// </summary>
    public string UrlBase { get; set; } = string.Empty;

    /// <summary>
    /// Nome da conexão.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Lista com os endpoints da aplicação consumida.
    /// </summary>
    public List<string> Endpoints { get; set; } = [];
}
