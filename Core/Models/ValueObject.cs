// -----------------------------------------------------------------------
// <copyright file="ValueObject.cs"  company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Models;

/// <summary>
/// Classe base para objetos de valor.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Retorna se objeto a ser comparado com atual é igual.
    /// </summary>
    /// <param name="obj">
    /// Objeto a ser comparado.
    /// </param>
    /// <returns>
    /// Verdadeiro caso igual.
    /// Falso caso diferente.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        ValueObject other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Gera um hash baseado nos itens do objeto.
    /// </summary>
    /// <returns>
    /// Retorna o hash da operação.
    /// </returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Verifica se objeto de valor é igual.
    /// </summary>
    /// <param name="left">
    /// Entidade a esquerda.
    /// </param>
    /// <param name="right">
    /// Entidade a direita.
    /// </param>
    /// <returns>
    /// True: Objeto de valor igual.
    /// False: Objeto de valor diferente.
    /// </returns>
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, null) ?
            Equals(right, null) :
            left.Equals(right);
    }

    /// <summary>
    /// Verifica se o objeto de valor é diferente.
    /// </summary>
    /// <param name="left">
    /// Objeto de valor a esquerda.
    /// </param>
    /// <param name="right">
    /// Objeto de valor a direita.
    /// </param>
    /// <returns>
    /// True: Objeto de valor diferente.
    /// False: Objeto de valor igual.
    /// </returns>
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Busca componentes dos objetos.
    /// </summary>
    /// <returns>
    /// Retorna lista dos componentes.
    /// </returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
}
