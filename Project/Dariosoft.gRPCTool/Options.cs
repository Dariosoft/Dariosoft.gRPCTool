namespace Dariosoft.gRPCTool
{
    public class Options
    {
        public string NugetPackagesDirectory { get; set; } = "";

        public string[] AssemblySearchPaths { get; set; } = [];

        public string[] AssemblyFiles { get; set; } = [];
    }
}