namespace Dariosoft.gRPCTool.Factories
{
    public interface IEnumMessageFactory
    {
        Models.IEnumMessageElement Create(ElementTypes.EnumElement element);

        void Flush();
    }

    internal class EnumMessageFactory(
        INameFactory nameFactory,
        Accessories.IEnumFactoryQueueReader feed,
        Accessories.IElementPool pool
        ) : IEnumMessageFactory
    {
        public Models.IEnumMessageElement Create(ElementTypes.EnumElement element)
        {

            var name = nameFactory.Create(element);
          
            var items = Accessories.EnumHelper.Instance.GetItems(element.Source)
                .ToDictionary(e => e.Key, e => $"{name.Name}__{e.Value}");

            if (!items.ContainsKey(0))
                items.Add(0, $"{name.Name}_Unknown");

            var model = new Models.EnumMessage(element)
            {
                Name = name,
                Items = items.OrderBy(e => e.Value).ToDictionary(e => e.Key, e => e.Value)
            };

            pool.Add(model);

            return model;
        }

        public void Flush()
        {
            while (feed.Read() is { } item)
                Create(item);
        }
    }
}
