namespace Dariosoft.gRPCTool.Utilities
{
    public class ComponentList<T>()
        : HashSet<T>(new ComponentEqulaityComparer<T>())
        where T : Components.IComponent

    {
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items.Where(i => i is not null))
            {
                Add(item);
            }
        }
    }
    
}