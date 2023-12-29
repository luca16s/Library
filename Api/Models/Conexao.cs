// -----------------------------------------------------------------------
// <copyright file="Conexao.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

public class Conexao
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
