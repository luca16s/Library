namespace Web.Models
{
    public class Conexao
    {
        /// <summary>
        /// Url de conexão podendo ser com Banco de Dados ou API.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Nome da conexão.
        /// </summary>
        public string Nome { get; set; } = string.Empty;
    }
}
