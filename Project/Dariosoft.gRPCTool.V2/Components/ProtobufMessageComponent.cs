using Dariosoft.gRPCTool.V2.Models;

namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufMessageComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        public required Elements.MessageElement Source { get; init; }
        public required NameModel Name { get; init; }

        public bool IsEmptyMessage => Source.MessageType == typeof(void) || Source.MessageType == typeof(Task) || Source.MessageType == typeof(ValueTask);
        
        public override string ToString() => Name.ProtobufName;
    }
}