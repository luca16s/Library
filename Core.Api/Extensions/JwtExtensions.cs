// -----------------------------------------------------------------------
// <copyright file="JwtExtensions.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Api.Extensions;

using Core.Api.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class JwtExtensions
{
    /// <summary>
    /// Cria o token JWT.
    /// </summary>
    /// <param name="_">
    /// Informações de identidade.
    /// </param>
    /// <param name="jwt">
    /// Configurações para criação do token JWT.
    /// </param>
    /// <returns></returns>
    public static string CreateToken(
        this ClaimsIdentity? _,
        Jwt jwt
    ) => new JwtSecurityTokenHandler().WriteToken(
        new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            expires: DateTime.Now.AddMinutes(jwt.ExpireInMinutes),
            signingCredentials: new Signing(jwt.Secret).Credentials
        )
    );
}
