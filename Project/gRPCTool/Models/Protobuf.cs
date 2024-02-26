using System.Reflection;

namespace Dariosoft.gRPCTool.Models
{
    public record Protobuf
    {
        public Assembly Assembly { get; init; } = null!;

        public ProtobufHeader Header { get; init; } = null!;

        public IReadOnlyList<ProtobufService> Services { get; init; } = [];

        public IReadOnlyList<ComplexMessage> Messages { get; init; } = [];

        public IReadOnlyList<EnumMessage> Enums { get; init; } = [];
    }
}
