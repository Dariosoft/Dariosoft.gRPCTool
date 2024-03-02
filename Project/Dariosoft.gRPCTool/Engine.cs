using System.Reflection;
using Microsoft.Extensions.Options;

namespace Dariosoft.gRPCTool
{
    public interface IEngine
    {
        void Start(params string[] assemblyFiles);
    }

    class Engine(
        IOptions<Options> options,
        Utilities.ILogger logger,
        Utilities.IAssemblyLoader assemblyLoader,
        Factories.IProtobufComponentFactory factory,
        Factories.IProtoFileFactory protoFileFactory
    ) : IEngine
    {
        public void Start(params string[] assemblyFiles)
        {
            logger.Info("The engine has started.");

            Assembly? assembly = null;
            var assemblyName = "";

            for (var i = 0; i < assemblyFiles.Length; i++)
            {
                assemblyName = Path.GetFileNameWithoutExtension(assemblyFiles[i]);

                logger.Info($"Try to load the assembly {assemblyName}");

                assembly = assemblyLoader.Load(assemblyFiles[i]);

                if (assembly is not null)
                {
                    logger.Info($"Starting generate protobuf from the assembly {assemblyName}");

                    Console.WriteLine("======= Begin of the Protobuf Content ===========");

                    var protobuf = factory.Create(assembly);
                    
                    using (var output = protoFileFactory.Create(protobuf))
                    {
                        output.Flush();
                        output.Close();
                    }
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