using System.Reflection;
using Utilities = Dariosoft.gRPCTool.V2.Utilities;
using Factories = Dariosoft.gRPCTool.V2.Factories;

namespace Dariosoft.TestConsole.V2
{
    public class OutputWriterFactory: Factories.IOutputWriterFactory
    {
        public Utilities.IOuputWriter Create(Dariosoft.gRPCTool.V2.Components.ProtobufComponent component)
        {
            var filename = Path.GetFileNameWithoutExtension(component.Source.GetName().Name)!.Replace('.', '_');

            filename = @$"D:\Dariosoft\Projects\2024\Dariosoft.gRPCTool\Project\TestConsole\Protos\{filename}.proto";
            
            return new MultiTextWriter(Console.Out, new StreamWriter(filename));
        }
    }
}