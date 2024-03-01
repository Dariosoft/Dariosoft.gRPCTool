namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IProtobufMessageComponentFactory
    {
        Components.IMessageComponent Create(Elements.Element element, Delegates.EnqueueElement enqueue);
    }

    class ProtobufMessageComponentFactory(
        MessageCreationStrategies.RequestMessageStrategy requestMessageStrategy, 
        MessageCreationStrategies.ReplyMessageStrategy replyMessageStrategy,
        MessageCreationStrategies.DataMessageStrategy dataMessageStrategy,
        MessageCreationStrategies.EnumMessageStrategy enumMessageStrategy) : IProtobufMessageComponentFactory
    {
        public Components.IMessageComponent Create(Elements.Element element, Delegates.EnqueueElement enqueue)
        {
            return element switch
            {
                Elements.RequestMessageElement requestMessage => requestMessageStrategy.Create(requestMessage, enqueue),
                Elements.MessageElement { Type: Enums.ElementType.ReplyMessage } messageElement => replyMessageStrategy.Create(messageElement, enqueue),
                Elements.MessageElement { Type: Enums.ElementType.Message } messageElement => dataMessageStrategy.Create(messageElement, enqueue),
                Elements.EnumElement { Type: Enums.ElementType.Enum } messageElement => enumMessageStrategy.Create(messageElement),
                _ => throw new ArgumentException("Not supported element", nameof(element))
            };
        }
    }
}