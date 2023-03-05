namespace Web.Helpers
{
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Classe de ajuda para configurações.
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// Busca a versão do assembly.
        /// </summary>
        /// <returns>
        /// Versão do assembly ou string vazia caso nulo.
        /// </returns>
        public static string GetAssemblyVersion()
        {
            return Assembly.GetCallingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Busca o nome da companhia no assembly.
        /// </summary>
        /// <returns>
        /// Nome da companhia ou string vazia caso nulo.
        /// </returns>
        public static string GetCompanyName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).CompanyName ?? string.Empty;
        }

        /// <summary>
        /// Busca a descrição do aplicativo no assembly.
        /// </summary>
        /// <returns>
        /// Descrição do aplicativo ou string vazia caso nulo.
        /// </returns>
        public static string GetAppDescription()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).Comments ?? string.Empty;
        }

        /// <summary>
        /// Busca o nome do aplicativo no assembly.
        /// </summary>
        /// <returns>
        /// Nome do aplicativo ou string vazia caso nulo.
        /// </returns>
        public static string GetAppName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).ProductName ?? string.Empty;
        }
    }
}
