namespace Dariosoft.gRPCTool.Factories
{
    public interface IMessageFactory
    {
        Models.IMessageElement Create(ElementTypes.IElement element);

        void Flush();
    }

    internal class MessageFactory(
        Analyzers.ITypeAnalyzer typeAnalyzer,
        Accessories.IElementPool pool,
        Accessories.IMessageFactoryQueueReader factoryQueueReader,
        IEnumerable<MessageBuilders.IMessageBuildStrategy> strategies) : IMessageFactory
    {
        public Models.IMessageElement Create(ElementTypes.IElement element)
        {
            var model = strategies.FirstOrDefault(e => e.IsResponsible(element))?.Create(element)
                ?? throw new NotSupportedException($"No message builder is supports the type \"{element.Type}\".");

            pool.Add(model);
            
            return model;
            

        }

        // public Models.IMessageElement Create(Type messageType)
        //     => Create(typeAnalyzer.Analyze(messageType));

        public void Flush()
        {
            while (factoryQueueReader.Read() is { } item)
                Create(item);
        }

        // private Models.IMessageElement Create(Models.TypeDescriptor type)
        // {
        //     if (type.IsVoid) return new Models.EmptyMessage();
        //     var model = strategySelector.Select(type).Create(type);
        //     pool.Add(model);
        //     return model;
        // }
    }
}