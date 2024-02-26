namespace Dariosoft.gRPCTool.Accessories
{
    public interface IMessageFactoryQueueFeeder : ITypeQFeeder<ElementTypes.MessageElement> { }

    public interface IMessageFactoryQueueReader : ITypeQReader<ElementTypes.MessageElement> { }

    internal class MessageFactoryQueue : TypeQueue<ElementTypes.MessageElement>, IMessageFactoryQueueFeeder, IMessageFactoryQueueReader
    {
    }
}
