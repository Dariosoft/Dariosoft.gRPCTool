namespace Dariosoft.gRPCTool.Utilities
{
    public class ComponentEqulaityComparer<T> : IEqualityComparer<T>
        where T : Components.IComponent
    {
        public bool Equals(T? x, T? y) => (x is null ? 0 : GetHashCode(x)) == (y is null ? 0 : GetHashCode(y));

        public int GetHashCode(T obj) => obj.Id.GetHashCode();
    }
}