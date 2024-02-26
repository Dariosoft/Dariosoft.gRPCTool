namespace Dariosoft.gRPCTool.ElementTypes
{
    public record RpcReplyMessageElement : Element<Type>, IElement<Type>
    {
        public RpcReplyMessageElement(Type source) : base(source, Enums.ElementType.RpcReplyMessage)
        {
        }
    }
}