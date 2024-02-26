namespace Dariosoft.gRPCTool.Models
{
    public record ProtobufHeader
    {
        public required string ProtobufSyntax { get; init; } = "";
        
        public required string ProtobufPackage { get; init; } = "";
        
        public string? CSharpNameSpace { get; init; }
        
        public string[] Imports { get; init; } = [];
    }
}
