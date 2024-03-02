using Microsoft.Extensions.Configuration;

namespace iosoft.gRPCTool.Console
{
    static class Extensions
    {
        public static string GetNugetPackagesDirectory(this IConfiguration configuration)
        {
            var value = configuration["nugets-path"];

            if(string.IsNullOrWhiteSpace(value))
                value = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");

            return value;
        }

        public static string[] GetAssemblySearchPath(this IConfiguration configuration)
        {
            return (configuration["assembly-search-path"] ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries);
                
        }

        public static string[] GetAssemblyFiles(this IConfiguration configuration)
        {
            return (configuration["assembly"] ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries);

        }
    }
}