// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Models
{
    using FluentValidation.Results;

    /// <summary>
    /// Entidade base.
    /// </summary>
    /// <typeparam name="TId">
    /// Tipo do identificador da entidade.
    /// </typeparam>
    public abstract class Entity<TId> where TId : struct
    {
        private int? _requestedHashCode;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="Entity" />.
        /// Construtor com identificador passado via parametro.
        /// </summary>
        /// <param name="id">
        /// Identificador.
        /// </param>
        public Entity(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Obtém identificador da entidade.
        /// </summary>
        public TId Id { get; private set; }

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
            if (obj is null) return false;

            if (obj is not Entity<TId> || GetType() != obj.GetType()) return false;

            if (ReferenceEquals(this, obj)) return true;

            Entity<TId> item = (Entity<TId>)obj;

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
            if (validationResult is null) return;

            foreach (ValidationFailure? error in validationResult.Errors)
            {
                if (error is null) continue;

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
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
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
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }
    }
}
