// -----------------------------------------------------------------------
// <copyright file="EnumModel.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Models
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
