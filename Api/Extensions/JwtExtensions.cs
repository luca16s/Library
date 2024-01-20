// -----------------------------------------------------------------------
// <copyright file="JwtExtensions.cs" company="Îakaré Softwareoka Inc.">
//     Copyright (c) Îakaré Softwareoka Inc..
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Extensions;

using Api.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public static class JwtExtensions
{
    public static string CreateJwtToken(
        this ClaimsIdentity? _,
        Settings settings
    )
    {
        Signing signing = new(settings.Secret);

        var token = new JwtSecurityToken(
            issuer: settings.Jwt.Issuer,
            audience: settings.Jwt.Audience,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: signing.Credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
