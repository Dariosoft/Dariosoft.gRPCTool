namespace Dariosoft.gRPCTool.Elements
{
    public class EnumElement : Element
    {
        public EnumElement(Type type)
            : base(type.IsEnum ? type : throw new ArgumentException("The type is not an enum.", nameof(type)), Enums.ElementType.Enum)
        {
            MessageType = type;
        }
        
        public Type MessageType { get; }
    }
}