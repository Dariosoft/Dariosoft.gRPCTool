using System.Diagnostics.CodeAnalysis;

namespace Dariosoft.gRPCTool.Accessories
{
    public interface IElementPool
    {
        void Add(Models.IProtobufElement item);

        void Clear();

        IEnumerable<Models.IProtobufElement> Items { get; }
    }

    internal class ElementPool : IElementPool
    {
        private readonly HashSet<Models.IProtobufElement> _items = new HashSet<Models.IProtobufElement>(new ProtobufElementEqualityComparer());

        public void Add(Models.IProtobufElement item)
        {
            if (item is not null)
                _items.Add(item);
        }

        public void Clear() => _items.Clear();

        public IEnumerable<Models.IProtobufElement> Items => _items;
    }

    class ProtobufElementEqualityComparer : IEqualityComparer<Models.IProtobufElement>
    {
        public bool Equals(Models.IProtobufElement? x, Models.IProtobufElement? y)
            => (x is null ? 0 : GetHashCode(x)) == (y is null ? 0 : GetHashCode(y));

        public int GetHashCode([DisallowNull] Models.IProtobufElement obj)
            => obj.HashKey.GetHashCode();
    }
}
