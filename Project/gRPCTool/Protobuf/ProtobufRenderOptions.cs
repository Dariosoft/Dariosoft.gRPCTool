using System.Reflection;

namespace Dariosoft.gRPCTool.Protobuf
{
    public record ProtobufRenderOptions(Assembly Assembly)
    {
        /// <summary>
        /// Indicates the protobuf package name.
        /// </summary>
        public string Package { get; set; } = "";

        /// <summary>
        /// <para>Indicates a list of external protobufs to be imported in the current generated protobuf.</para>
        /// <para>This list by default contains the standard google protobufs such as google/protobuf/timestamp.proto, google/protobuf/any.proto, ...</para>
        /// </summary>
        public List<string> Imports { get; } = new();
    }
}
