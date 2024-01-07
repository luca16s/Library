// -----------------------------------------------------------------------
// <copyright file="Jwt.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

using System;
using System.Threading.Tasks;

public class Jwt
{
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int ValidForMinutes { get; set; }

    public static DateTime IssuedAt => DateTime.UtcNow;
    public static DateTime NotBefore => DateTime.UtcNow;
    public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForMinutes);
    public DateTime Expiration => IssuedAt.AddMinutes(ValidFor.TotalMinutes);

    public static Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
}
