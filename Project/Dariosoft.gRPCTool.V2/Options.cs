namespace Dariosoft.gRPCTool.V2
{
    public class Options
    {
        public string NugetPackagesDirectory { get; set; } = "";

        public string[] AssemblySearchPaths { get; set; } = [];

        public string[] AssemblyFiles { get; set; } = [];
    }
}