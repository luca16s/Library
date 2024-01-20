// -----------------------------------------------------------------------
// <copyright file="Connection.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

public class Connection
{
    /// <summary>
    /// Url de conexão podendo ser com Banco de Dados ou API.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Nome da conexão.
    /// </summary>
    public string Nome { get; set; } = string.Empty;
}
