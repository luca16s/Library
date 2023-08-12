// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CrossCutting.ViewModels
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// ViewModel base.
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// Identificador padrão de entidades.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
    }
}
