using System.Reflection;

namespace Dariosoft.gRPCTool.Factories
{
    public interface IProtobufHeaderFactory
    {
        Models.ProtobufHeader Create(Assembly assembly);
    }

    class ProtobufHeaderFactory : IProtobufHeaderFactory
    {
        public Models.ProtobufHeader Create(Assembly assembly)
        {
            return new Models.ProtobufHeader
            {
                ProtobufSyntax = "proto3",
                ProtobufPackage = $"{assembly.GetName().Name}.GrpcInterface".TrimStart(' ', '.'),
                CSharpNameSpace = null,
                Imports = Protobuf.GoogleProtobuf.Protobufs
            };
        }
    }
}
