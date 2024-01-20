// -----------------------------------------------------------------------
// <copyright file="Signing.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

using Microsoft.IdentityModel.Tokens;

using System.Text;

/// <summary>
/// Monta as credenciais da aplicação.
/// </summary>
/// <param name="secret">
/// Segredo para montagem da chave.
/// </param>
public class Signing(
    string secret
)
{
    public SigningCredentials Credentials { get; } = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            SecurityAlgorithms.HmacSha256Signature
    );
}
