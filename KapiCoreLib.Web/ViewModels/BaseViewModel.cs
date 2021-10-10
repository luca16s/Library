// -----------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Îakaré Software'oka">
//     Copyright (c) Îakaré Software'oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace KapiCoreLib.Web.ViewModels
{
    using System;

    using Microsoft.AspNetCore.Mvc;

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
