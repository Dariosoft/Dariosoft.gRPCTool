using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.ElementNameBuilders
{
    public interface INameBuildStrategy
    {
        Enums.ElementType ResponsibleFor { get; }
        
        Models.ItemName Create(MemberInfo member);
    }
}