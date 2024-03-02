using System.Reflection;

namespace Dariosoft.gRPCTool.Components
{
    
    public class ProtobufComponent
    {
        public required Assembly Source { get; init; }

        public required string ProtobufSyntax { get; init; }

        public required string ProtobufPackage { get; init; }

        public string? CSharpNameSpace { get; init; }

        public HashSet<string> Imports { get; } = [];

        public Utilities.ComponentList<ProtobufServiceComponent> Services { get; } = [];

        public Utilities.ComponentList<ProtobufEnumComponent> Enums { get; } = [];

        public Utilities.ComponentList<ProtobufMessageComponent> Messages { get; } = [];
    }
}