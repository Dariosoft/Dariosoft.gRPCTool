using System.Reflection;
using Dariosoft.gRPCTool.Components;
using Utilities = Dariosoft.gRPCTool.Utilities;
using Factories = Dariosoft.gRPCTool.Factories;
using IOuputWriter = Dariosoft.gRPCTool.Utilities.IOuputWriter;
using IOutputWriterFactory = Dariosoft.gRPCTool.Factories.IOutputWriterFactory;

namespace Dariosoft.TestConsole
{
    public class OutputWriterFactory: IOutputWriterFactory
    {
        public IOuputWriter Create(ProtobufComponent component)
        {
           var filename = Path.GetFileNameWithoutExtension(component.Source.GetName().Name)!.Replace('.', '_');

            filename = @$"D:\Dariosoft\Projects\2024\Dariosoft.gRPCTool\Project\TestConsole\Protos\{filename}.proto";
            
            return new MultiTextWriter(Console.Out, new StreamWriter(filename));
        }
    }
}