namespace Dariosoft.gRPCTool.Models
{
    public record EnumMessage : IEnumMessageElement
    {
        public EnumMessage(ElementTypes.EnumElement element)
        {
            this.Element = element;
        }
        public ElementTypes.IElement Element { get; }
        
        public required ItemName Name { get; init; } = null!;

        public string HashKey => (Name?.ProtobufName ?? "").ComputeHash();

        public IDictionary<int, string> Items { get; init; } = null!;

        public override string ToString() => Name?.Name ?? "";
    }
}
