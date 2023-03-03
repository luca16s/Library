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
