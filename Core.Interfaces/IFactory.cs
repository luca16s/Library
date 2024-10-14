// -----------------------------------------------------------------------
// <copyright file="IFactory.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Interfaces;

/// <summary>
/// Interface para criação de fábricas.
/// </summary>
/// <typeparam name="TEntity">
/// Tipo que define entidade a ser instanciada.
/// </typeparam>
/// <typeparam name="TReturn">
/// Entidade Instanciada.
/// </typeparam>
public interface IFactory<TEntity, out TReturn>
{
    /// <summary>
    /// Instancia um tipo concreto.
    /// </summary>
    /// <param name="entity">
    /// Entidade a ser instanciada.
    /// </param>
    /// <returns>
    /// <see cref="TEntity"> Tipo concreto.
    /// </returns>
    TReturn Create(TEntity entity);
}
