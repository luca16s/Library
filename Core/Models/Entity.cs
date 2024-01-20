// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Models;

using FluentValidation.Results;

/// <summary>
/// Entidade base.
/// </summary>
/// <remarks>
/// Inicia uma nova instância da classe <see cref="Entity"/>.
/// Construtor com identificador passado via parametro.
/// </remarks>
/// <param name="id">
/// Identificador.
/// </param>
public abstract class Entity(long id)
{
    private int? _requestedHashCode;

    /// <summary>
    /// Obtém identificador da entidade.
    /// </summary>
    public long Id { get; private set; } = id;

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
        {
            _requestedHashCode = Id.GetHashCode() ^ 31;
        }

        return _requestedHashCode.Value;
    }

    /// <summary>
    /// Verifica se entidade é igual.
    /// </summary>
    /// <param name="obj">
    /// Entidade a ser comparada.
    /// </param>
    /// <returns>
    /// True: Entidade igual.
    /// False: Entidade diferente.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Entity || GetType() != obj.GetType())
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        Entity item = (Entity)obj;

        return item.Id.Equals(Id);
    }

    /// <summary>
    /// Adiciona erros de validação ao validationResult da entidade.
    /// </summary>
    /// <param name="validationResult">
    /// Validação a ser adicionada.
    /// </param>
    public void AddValidationError(ValidationResult validationResult)
    {
        if (validationResult is null)
        {
            return;
        }

        foreach (ValidationFailure? error in validationResult.Errors)
        {
            if (error is null)
            {
                continue;
            }

            ValidationResult.Errors.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
        }
    }

    /// <summary>
    /// Verifica se entidade é igual.
    /// </summary>
    /// <param name="left">
    /// Entidade a esquerda.
    /// </param>
    /// <param name="right">
    /// Entidade a direita.
    /// </param>
    /// <returns>
    /// True: Entidade igual.
    /// False: Entidade diferente.
    /// </returns>
    public static bool operator ==(Entity left, Entity right)
    {
        return Equals(left, null) ?
            Equals(right, null) :
            left.Equals(right);
    }

    /// <summary>
    /// Verifica se entidade é diferente.
    /// </summary>
    /// <param name="left">
    /// Entidade a esquerda.
    /// </param>
    /// <param name="right">
    /// Entidade a direita.
    /// </param>
    /// <returns>
    /// True: Entidade diferente.
    /// False: Entidade igual.
    /// </returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
