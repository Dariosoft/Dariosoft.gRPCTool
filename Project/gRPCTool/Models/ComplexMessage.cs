
namespace Dariosoft.gRPCTool.Models
{
    public record ComplexMessage : IMessageElement
    {
        public ComplexMessage(ElementTypes.MessageElement element)
        {
            this.Element = element;
        }
        
        public ComplexMessage(ElementTypes.RpcRequestMessageElement element)
        {
            this.Element = element;
        }
        
        public ElementTypes.IElement Element { get; }
        
        public required ItemName Name { get; init; } = null!;

        public string HashKey => (Name?.ProtobufName ?? "").ComputeHash();

        public IReadOnlyList<MessageMember> Members { get; init; } = [];

        public override string ToString() => Name?.Name ?? "";
    }
}
