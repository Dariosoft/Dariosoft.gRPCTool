using System.Reflection;

namespace Dariosoft.gRPCTool.ElementTypes
{
    public interface IElement
    {
        Enums.ElementType Type { get; }
    }
    
    public interface IElement<out TSource> : IElement
        where TSource : MemberInfo
    {
        TSource Source { get; }
    }
}