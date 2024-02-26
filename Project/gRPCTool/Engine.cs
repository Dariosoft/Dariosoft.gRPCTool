using Microsoft.Extensions.Options;
using System.Reflection;

namespace Dariosoft.gRPCTool
{

    public interface IEngine
    {
        void Start(Accessories.ITextWriter textWriter);
    }

    internal class Engine(
        IOptions<Options> options,
        Accessories.ILogger logger,
        Accessories.IAssemblyLoader assemblyLoader,
        Factories.IProtobufFactory protobufFactory,
        ProtobufWriters.IProtobufWriter protobufWriter
        ) : IEngine
    {
        public void Start(Accessories.ITextWriter textWriter)
        {
            logger.Info("The engine has started.");
            Assembly? assembly = null;
            var assemblyName = "";

            for (int i = 0; i < options.Value.AssemblyFiles.Length; i++)
            {
                assemblyName = Path.GetFileNameWithoutExtension(options.Value.AssemblyFiles[i]);

                logger.Info($"Try to load the assembly {assemblyName}");

                assembly = assemblyLoader.Load(options.Value.AssemblyFiles[i]);

                if (assembly is not null)
                {
                    logger.Info($"Starting generate protobuf from the assembly {assemblyName}");

                    Console.WriteLine("======= Begin of the Protobuf Content ===========");

                    var protobuf = protobufFactory.Create(assembly);
                    
                    protobufWriter.Write(textWriter, protobuf);

                    Console.WriteLine("======= End of the Protobuf Content ===========");
                }
                else
                {
                    logger.Warning($"Assembly has been skipped due to error: {assemblyName}");
                }
                // D:\Dariosoft\Projects\2024\Dariosoft.gRPCTool\Project\TestConsole
            }
        }
    }
}
