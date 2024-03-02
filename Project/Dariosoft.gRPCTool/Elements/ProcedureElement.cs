using System.Reflection;

namespace Dariosoft.gRPCTool.Elements
{
    public class ProcedureElement : Element
    {
        public ProcedureElement(MethodInfo methodInfo)
            : base(methodInfo, Enums.ElementType.Procedure)
        {
            this.MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }
    }
}