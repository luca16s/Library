// -----------------------------------------------------------------------
// <copyright file="EnumItemNotFoundException.cs" company="Îakaré Software'oka">
//     Copyright (c) Îakaré Software'oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace KapiCoreLib.Exceptions
{
    /// <summary>
    /// Exceção caso item do enum não tenha sido encontrado.
    /// </summary>
    public class EnumItemNotFoundException : Exception
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
        /// </summary>
        private const string DefaultMessage = "Item não encontrado no enumerador.";

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
        /// </summary>
        public EnumItemNotFoundException()
            : base(DefaultMessage) { }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
        /// </summary>
        /// <param name="message">
        /// Mensagem a ser mostrada.
        /// </param>
        public EnumItemNotFoundException(string message)
            : base($"{DefaultMessage}\n - {message}") { }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumItemNotFoundException" />.
        /// </summary>
        /// <param name="message">
        /// Mensagem a ser mostrada.
        /// </param>
        /// <param name="inner">
        /// Mensagem herdada.
        /// </param>
        public EnumItemNotFoundException(string message, Exception inner)
            : base($"{DefaultMessage}\n - {message}", inner) { }
    }
}
