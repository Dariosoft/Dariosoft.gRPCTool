using System.Reflection;

namespace Dariosoft.gRPCTool.Elements
{
    public class RequestMessageElement : Element
    {
        public RequestMessageElement(MethodInfo methodInfo, Models.XParameterInfo[] parameters)
            : base(methodInfo, Enums.ElementType.RequestMessage)
        {
            this.MethodInfo = methodInfo;
            this.Parameters = parameters;
        }

        public MethodInfo MethodInfo { get; }
        
        public Models.XParameterInfo[] Parameters { get; }
    }
}