namespace Dariosoft.gRPCTool.ElementTypes
{
    public record MessageElement : Element<Type>, IElement<Type>
    {
        public MessageElement(Models.TypeDescriptor source) : base(source.Type, Enums.ElementType.Message)
        {
            this.Descriptor = source;
        }
        
        public Models.TypeDescriptor Descriptor { get; }
    }
}