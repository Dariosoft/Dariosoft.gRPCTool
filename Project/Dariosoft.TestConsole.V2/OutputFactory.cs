using System.Reflection;
using Utilities = Dariosoft.gRPCTool.V2.Utilities;
using Factories = Dariosoft.gRPCTool.V2.Factories;

namespace Dariosoft.TestConsole.V2
{
    public class OutputFactory: Factories.IOutputFactory
    {
        public Utilities.IOuput Create(Assembly assembly)
        {
            var filename = Path.GetFileNameWithoutExtension(assembly.FullName)!.Replace('.', '_');

            filename = @$"D:\Dariosoft\Projects\2024\Dariosoft.gRPCTool\Project\TestConsole\Protos\{filename}.proto";
            
            return new MultiTextWriter(Console.Out, new StreamWriter(filename));
        }
    }
}