namespace Web.Models
{
    /// <summary>
    /// Informações para preenchimento do swagger.
    /// </summary>
    public class SwaggerInformation
    {
        /// <summary>
        /// Site da aplicação.
        /// </summary>
        public string? Site { get; set; } = string.Empty;

        /// <summary>
        /// E-Mail para contato com responsável pela aplicação.
        /// </summary>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public string? AppName { get; set; } = string.Empty;

        /// <summary>
        /// Versão da aplicação.
        /// </summary>
        public string? Version { get; set; } = string.Empty;

        /// <summary>
        /// Nome da companhia responsável pela aplicação.
        /// </summary>
        public string? Company { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da aplicação.
        /// </summary>
        public string? Description { get; set; } = string.Empty;
    }
}
