using System.Reflection;
using Microsoft.Extensions.Options;

namespace Dariosoft.gRPCTool.V2
{
    public interface IEngine
    {
        void Start();
    }

    class Engine(
        IOptions<Options> options,
        Utilities.ILogger logger,
        Utilities.IAssemblyLoader assemblyLoader,
        Factories.IProtobufComponentFactory factory
        ): IEngine
    {
        public void Start()
        {
            logger.Info("The engine has started.");

            Assembly? assembly = null;
            var assemblyName = "";
            
            for (var i = 0; i < options.Value.AssemblyFiles.Length; i++)
            {
                assemblyName = Path.GetFileNameWithoutExtension(options.Value.AssemblyFiles[i]);

                logger.Info($"Try to load the assembly {assemblyName}");

                assembly = assemblyLoader.Load(options.Value.AssemblyFiles[i]);
                
                if (assembly is not null)
                {
                    logger.Info($"Starting generate protobuf from the assembly {assemblyName}");

                    Console.WriteLine("======= Begin of the Protobuf Content ===========");

                    var protobuf = factory.Create(assembly);
                   // protobufWriter.Write(textWriter, protobuf);

                    Console.WriteLine("======= End of the Protobuf Content ===========");
                }
                else
                {
                    logger.Warning($"Assembly has been skipped due to error: {assemblyName}");
                }
            }
        }
    }
}