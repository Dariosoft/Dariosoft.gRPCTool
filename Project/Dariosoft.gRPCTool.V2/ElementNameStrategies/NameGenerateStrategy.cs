using System.Reflection;

namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public abstract class NameGenerateStrategy<T> : INameGenerateStrategy
        where T : MemberInfo
    {
        public bool Enabled { get; protected set; }

        public int Priority { get; protected set; }

        public abstract Enums.ElementType ElementType { get; }

        public Models.NameModel Create(Elements.Element element)
        {
            if (element.Type != ElementType || element.Target is not T info)
            {
                string t_full_name = typeof(T).FullName
                    , target_full_name = element.Target.GetType().FullName;
                throw new ArgumentException("Mismatch target.", nameof(element));
            }

            return Create(element, info);
        }

        protected abstract Models.NameModel Create(Elements.Element element, T target);
    }
}