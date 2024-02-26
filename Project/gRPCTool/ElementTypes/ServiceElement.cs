using System.Reflection;

namespace Dariosoft.gRPCTool.ElementTypes
{
    public record ServiceElement : Element<Type>, IElement<Type>
    {
        public ServiceElement(Type source)
            : base(source, Enums.ElementType.Service)
        {
        }
    }
}