using Dariosoft.gRPCTool.V2.Models;

namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufEnumComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        
        public required Type Source { get; init; }
        public required NameModel Name { get; init; }
        
        public override string ToString() => Name.ProtobufName;

        
    }
}