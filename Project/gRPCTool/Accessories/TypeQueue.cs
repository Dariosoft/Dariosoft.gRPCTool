namespace Dariosoft.gRPCTool.Accessories
{
    public interface ITypeQFeeder<in T>  
        where T: ElementTypes.IElement
    {
        void Enqueue(T element);
    }

    public interface ITypeQReader<out T>  
        where T: ElementTypes.IElement
    {
        T? Read();
    }


    abstract class TypeQueue<T> : ITypeQFeeder<T>, ITypeQReader<T>
        where T: ElementTypes.IElement
    {
        private readonly HashSet<T> _items = new();
        private int position = 0;

        public virtual void Enqueue(T element)
        {
            if (element is not null)
                _items.Add(element);
        }

        public T? Read()
        {
            if (position >= _items.Count) return default;

            var item = _items.ElementAt(position++);

            return item;
        }
    }

    

}
