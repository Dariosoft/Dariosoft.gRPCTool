using System.Reflection;

namespace Dariosoft.gRPCTool.ElementTypes
{
    public record ProcedureElement : Element<MethodInfo>, IElement<MethodInfo>
    {
        public ProcedureElement(MethodInfo source)
            : base(source, Enums.ElementType.Procedure)
        {
            RequestMessageElement = new RpcRequestMessageElement(source);
            ReplyMessageElement = new RpcReplyMessageElement(source.ReturnType);
        }
        
        public RpcRequestMessageElement RequestMessageElement { get; }
        
        public RpcReplyMessageElement ReplyMessageElement { get; }
    }
}