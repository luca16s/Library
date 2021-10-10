// -----------------------------------------------------------------------
// <copyright file="IFileService.cs" company="Îakaré Software'oka">
//     Copyright (c) Îakaré Software'oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------
namespace KapiCoreLib.Desktop.Interfaces.Services
{
    /// <summary>
    /// Interface para operações com arquivos.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Apagar pasta ou arquivo.
        /// </summary>
        /// <param name="folderPath">
        /// Caminho da pasta.
        /// </param>
        /// <param name="fileName">
        /// Nome do arquivos.
        /// </param>
        void Delete(string folderPath, string fileName);

        /// <summary>
        /// Ler arquivos.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de arquivo.
        /// </typeparam>
        /// <param name="folderPath">
        /// Caminho da pasta.
        /// </param>
        /// <param name="fileName">
        /// Nome do arquivo.
        /// </param>
        /// <returns>Arquivos lidos.</returns>
        T Read<T>(string folderPath, string fileName);

        /// <summary>
        /// Salvar arquivos.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo de arquivo.
        /// </typeparam>
        /// <param name="folderPath">
        /// Caminho da pasta.
        /// </param>
        /// <param name="fileName">
        /// Nome do arquivo.
        /// </param>
        /// <param name="content">
        /// Conteudo a ser salvo.
        /// </param>
        void Save<T>(string folderPath, string fileName, T content);
    }
}
