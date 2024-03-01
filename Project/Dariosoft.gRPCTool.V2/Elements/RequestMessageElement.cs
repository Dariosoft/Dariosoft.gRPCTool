using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Elements
{
    public class RequestMessageElement : Element
    {
        public RequestMessageElement(MethodInfo methodInfo)
            : base(methodInfo, Enums.ElementType.RequestMessage)
        {
            this.MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }

        public bool HasParameter() => MethodInfo.GetParameters().Any(p => !p.IsOut);
    }
}