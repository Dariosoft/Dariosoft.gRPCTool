using System.Reflection;
using Dariosoft.gRPCTool.V2.Components;

namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IProtobufComponentFactory
    {
        ProtobufComponent Create(Assembly assembly);
    }

    class ProtobufComponentFactory(Composers.ProtobufImportsComposer composer) : IProtobufComponentFactory
    {
        public ProtobufComponent Create(Assembly assembly)
        {
            var component = new ProtobufComponent
            {
                Source = assembly,
                ProtobufSyntax = "proto3",
                ProtobufPackage = $"{assembly.GetName().Name}.GrpcInterface".TrimStart(' ', '.')
            };

            composer.Accept(component);
            
            return component;
        }
    }
}