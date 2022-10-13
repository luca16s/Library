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

    /// <summary>
    /// ViewModel base.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador.
    /// </typeparam>
    public class ViewModel<TId> where TId : struct
    {
        /// <summary>
        /// Identificador padrão de entidades.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public TId Id { get; set; }
    }
}
