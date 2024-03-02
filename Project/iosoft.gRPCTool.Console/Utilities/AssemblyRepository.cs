using Microsoft.Extensions.Options;

namespace iosoft.gRPCTool.Console.Utilities
{
    public record AssemblyRepositoryItem(string AssemblyName, string SearchPath);
    
    public class AssemblyRepositoryOptions
    {
        public List<AssemblyRepositoryItem> Items { get; } = [];
    }
    
    public class AssemblyRepository(IOptions<AssemblyRepositoryOptions> options)
    {
        public IReadOnlyList<AssemblyRepositoryItem> Items => options.Value.Items;
    }
}