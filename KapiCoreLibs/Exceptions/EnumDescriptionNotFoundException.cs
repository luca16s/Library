// -----------------------------------------------------------------------
// <copyright file="EnumDescriptionNotFoundException.cs" company="Îakaré Software'oka">
//     Copyright (c) Îakaré Software'oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace KapiCoreLib.Exceptions
{
    /// <summary>
    /// Exceção caso descrição do enum não tenha sido encontrada.
    /// </summary>
    public class EnumDescriptionNotFoundException : Exception
    {
        private const string DefaultMessage = "Enum informado não contém descrição.";

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumDescriptionNotFoundException" />.
        /// </summary>
        public EnumDescriptionNotFoundException()
            : base(DefaultMessage) { }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumDescriptionNotFoundException" />.
        /// </summary>
        /// <param name="message">
        /// Mensagem a ser mostrada.
        /// </param>
        public EnumDescriptionNotFoundException(string message)
            : base($"{DefaultMessage}\n - {message}") { }

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EnumDescriptionNotFoundException" />.
        /// </summary>
        /// <param name="message">
        /// Mensagem a ser mostrada.
        /// </param>
        /// <param name="inner">
        /// Mensagem herdada.
        /// </param>
        public EnumDescriptionNotFoundException(string message, Exception inner)
            : base($"{DefaultMessage}\n - {message}", inner) { }
    }
}
