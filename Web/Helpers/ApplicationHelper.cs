namespace Web.Helpers
{
    using System.Diagnostics;
    using System.Reflection;

    public static class ApplicationHelper
    {
        public static string GetAssemblyVersion()
        {
            return Assembly.GetCallingAssembly()?.GetName()?.Version?.ToString() ?? string.Empty;
        }

        public static string GetCompanyName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).CompanyName ?? string.Empty;
        }

        public static string GetAppDescription()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).Comments ?? string.Empty;
        }

        public static string GetAppName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? string.Empty).ProductName ?? string.Empty;
        }
    }
}
