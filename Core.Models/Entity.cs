// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Models;

using Core.Interfaces;

using FluentValidation.Results;

using System;

/// <summary>
/// Entidade base.
/// </summary>
/// <remarks>
/// Inicia uma nova instância da classe <see cref="Entity"/>.
/// Construtor com identificador passado via parametro.
/// </remarks>
/// <param name="Id">
/// Identificador.
/// </param>
public abstract class Entity<TId> : IEntity<TId>
    where TId : notnull
{
    private int? _requestedHashCode;

    protected Entity()
    {
        if (Id is null)
            throw new ArgumentNullException(nameof(Id));
    }

    protected Entity(TId id) => Id = id;

    /// <summary>
    /// Obtém identificador da entidade.
    /// </summary>
    public TId Id { get; set; }
    public long Version { get; set; }
    public bool IsDeleted { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? LastModifiedBy { get; set; }
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Lista com as validações executadas para entidade.
    /// </summary>
    public ValidationResult ValidationResult { get; private set; } = new ValidationResult();

    /// <summary>
    /// Gera o hash para a entidade.
    /// </summary>
    /// <returns>
    /// Hash da entidade.
    /// </returns>
    public override int GetHashCode()
    {
        if (!_requestedHashCode.HasValue)
            _requestedHashCode = Id.GetHashCode() ^ 31;

        return _requestedHashCode.Value;
    }

    /// <summary>
    /// Adiciona erros de validação ao validationResult da entidade.
    /// </summary>
    /// <param name="validationResult">
    /// Validação a ser adicionada.
    /// </param>
    public void AddValidationError(
        ValidationResult validationResult
    )
    {
        if (validationResult is null)
            return;

        foreach (ValidationFailure? error in validationResult.Errors)
        {
            if (error is null) continue;

            ValidationResult.Errors.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
        }
    }
}
