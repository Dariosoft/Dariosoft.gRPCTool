using System.Reflection;
using Dariosoft.gRPCTool.Factories.ElementNameBuilders;

namespace Dariosoft.gRPCTool.Factories
{
    public interface INameFactory
    {
        Models.ItemName Create<T>(ElementTypes.IElement<T> element) where T : MemberInfo;
    }

    class NameFactory(IEnumerable<INameBuildStrategy> strategies): INameFactory
    {
        public Models.ItemName Create<T>(ElementTypes.IElement<T> element)
            where T : MemberInfo
        {
            return strategies.FirstOrDefault(e => e.ResponsibleFor == element.Type)?.Create(element.Source) 
                   ?? throw new NotSupportedException($"No name-builder supports the element type \"{Enum.GetName(element.Type)}\".");
        }
    }
}