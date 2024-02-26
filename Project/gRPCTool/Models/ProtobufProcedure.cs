namespace Dariosoft.gRPCTool.Models
{
    public record ProtobufProcedure: IProcedureElement
    {
        public ProtobufProcedure(ElementTypes.ProcedureElement element)
        {
            this.Element = _element = element;
        }

        private readonly ElementTypes.ProcedureElement _element;
        
        public ElementTypes.IElement Element { get; }

        public required ItemName Name { get; init; } = null!;

        public string HashKey => $"{_element.Source.DeclaringType!.FullName ?? _element.Source.DeclaringType.Name}.{Name.Name}.{_element.Source.GetHashCode()}".ComputeHash();

        public required IMessageElement RequestMessage { get; init; } = null!;

        public required IMessageElement ResponseMessage { get; init; } = null!;
    }
}
