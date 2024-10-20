// ------------------------------------------------------------------------------------
// <copyright file="DateExtensions.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------

namespace Core.Extensions;

using System;

/// <summary>
/// Classe de extensão para operações com DateTime.
/// </summary>
public static class DateExtensions
{
    /// <summary>
    /// Converte DateTime para formato UNIX.
    /// </summary>
    /// <param name="date">
    /// Data a ser convertida.
    /// </param>
    /// <returns>
    /// Total em segundos.
    /// </returns>
    public static double ToUnixEpoch(
        this DateTime date
    ) => ((DateTimeOffset)date.ToUniversalTime()).ToUnixTimeSeconds();

    /// <summary>
    /// Converte DateTime para formato UNIX em texto.
    /// </summary>
    /// <param name="date">
    /// Data a ser convertida.
    /// </param>
    /// <returns>
    /// Total em segundos em formato de texto.
    /// </returns>
    public static string ToUnixEpochToString(
        this DateTime date
    ) => date.ToUnixEpoch().ToString();
}
