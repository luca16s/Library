// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Api.Models;

using System.Collections.Generic;

public class Settings
{
    public Jwt Jwt { get; set; } = new Jwt();
    public string Secret { get; set; } = string.Empty;
    public Swagger Swagger { get; set; } = new Swagger();
    public string CorsPolicyName { get; set; } = string.Empty;
    public IEnumerable<string> AllowedDomains { get; set; } = new List<string>();
    public IEnumerable<Connection> ApiUrls { get; set; } = new List<Connection>();
    public IEnumerable<Connection> ConnectionStrings { get; set; } = new List<Connection>();
}
