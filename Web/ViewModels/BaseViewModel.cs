// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Web.ViewModels
{
    using Microsoft.AspNetCore.Mvc;

    using System;

    /// <summary>
    /// ViewModel base.
    /// </summary>
    public class BaseViewModel
    {
        /// <summary>
        /// Identificador padrão de entidades.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
    }
}
