// -----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Web.Models
{
    using System.Collections.Generic;

    public class Settings
    {
        public string ServerVersion { get; set; } = null!;
        public IEnumerable<Conexao> ApiUrls { get; set; } = new List<Conexao>();
        public IEnumerable<string> AllowedDomains { get; set; } = new List<string>();
        public IEnumerable<Conexao> ConnectionString { get; set; } = new List<Conexao>();
    }
}
