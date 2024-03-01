using Dariosoft.gRPCTool.V2.Models;

namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufEnumComponent: IMessageComponent
    {
        public string Id => Name.ProtobufName;
        public required NameModel Name { get; init; }
        
        //public required Type Source { get; init; }

        public required IDictionary<int, string> Members { get; init; }
        public override string ToString() => Name.ProtobufName;

        
    }
}