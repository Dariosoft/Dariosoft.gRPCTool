using System.Reflection;

namespace Dariosoft.gRPCTool.ElementTypes
{
    public record RpcRequestMessageElement : Element<MethodInfo>, IElement<MethodInfo>
    {
        public RpcRequestMessageElement(MethodInfo source)
            : base(source, Enums.ElementType.Procedure)
        {
            this.Parameters = source.GetParameters();
        }

        public ParameterInfo[] Parameters { get; }
    }
}