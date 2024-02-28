using System.Reflection;

namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public abstract class NameGenerateStrategy<T> : INameGenerateStrategy
        where T : MemberInfo
    {
        public bool Enabled { get; protected set; }

        public int Priority { get; protected set; }

        public abstract Enums.ElementType Target { get; }

        public Models.NameModel Create(Elements.Element element)
        {
            if (element.Type != Target && element.Target is not T)
                throw new ArgumentException("Mismatch target.", nameof(element));

            return Create(element, (element.Target as T)!);
        }

        protected abstract Models.NameModel Create(Elements.Element element, T target);
    }
}