namespace Dariosoft.gRPCTool.ElementTypes
{
    public record EnumElement : Element<Type>, IElement<Type>
    {
        public EnumElement(Type source)
            : base(source, Enums.ElementType.Enum)
        {
        }
    }
}