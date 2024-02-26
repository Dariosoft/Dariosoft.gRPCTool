using System.Reflection;

namespace Dariosoft.gRPCTool
{
    public static class MiscExtensions
    {
        public static string GetProtoPackageName(this Protobuf.ProtobufRenderOptions options)
        {
            return string.IsNullOrWhiteSpace(options.Package)
                ? options.Assembly.GetName().Name!
                : options.Package;
        }

        public static Protobuf.ProtobufRenderOptions GetProtobufRenderOptions(this Assembly assembly)
        {
            var options = new Protobuf.ProtobufRenderOptions(assembly)
            {
                Package = $"{assembly.GetName().Name}.GrpcInterface".TrimStart(' ', '.')
            };

            options.Imports.AddRange(Protobuf.GoogleProtobuf.Protobufs);

            //foreach (var visitor in optionVisitors.Where(x => x.Enabled))
            // visitor.Visit(options);

            return options;
        }
    }
}
