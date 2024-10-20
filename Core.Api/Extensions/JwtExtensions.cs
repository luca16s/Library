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
    public static string CreateJwtToken(
        this ClaimsIdentity? _,
        Settings settings
    ) => new JwtSecurityTokenHandler()
        .WriteToken(new JwtSecurityToken(
            issuer: settings.Jwt.Issuer,
            audience: settings.Jwt.Audience,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: new Signing(settings.Jwt.Secret).Credentials)
    );
}
