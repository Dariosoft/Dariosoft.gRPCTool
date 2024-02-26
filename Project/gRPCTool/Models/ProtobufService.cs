namespace Dariosoft.gRPCTool.Models
{
    public record ProtobufService : IServiceElement
    {
        public ProtobufService(ElementTypes.ServiceElement element)
        {
            this.Element = _element = element;
        }

        private readonly ElementTypes.ServiceElement _element;
        public ElementTypes.IElement Element { get; }
        
       // public required Type ServiceType { get; init; } = null!;

        public required ItemName Name { get; init; } = null!;

        public string HashKey => (_element.Source.FullName ?? _element.Source.Name).ComputeHash();

        public IReadOnlyList<ProtobufProcedure> Procedures { get; init; } = [];
    }
}
