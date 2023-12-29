// -----------------------------------------------------------------------
// <copyright file="SigningSettings.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.Text;

public class SigningSettings
{
    public SigningCredentials SigningCredentials { get; }

    public SigningSettings(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var secret = configuration[$"{nameof(SigningSettings)}:Secret"] ?? string.Empty;

        SymmetricSecurityKey? symmetricKey = new(Encoding.UTF8.GetBytes(secret));
        SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
    }
}
