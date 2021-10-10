// -----------------------------------------------------------------------
// <copyright file="EnumModel.cs" company="Îakaré Software'oka">
//     Copyright (c) Îakaré Software'oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace KapiCoreLib.Models
{
    /// <summary>
    /// Classe modelo para conversão de enumerador em lista.
    /// </summary>
    public class EnumModel
    {
        /// <summary>
        /// Descrição do enum.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Valor do enum.
        /// </summary>
        public Enum? Value { get; set; }
    }
}
