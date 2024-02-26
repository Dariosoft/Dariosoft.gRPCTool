namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    public interface IMessageBuilder
    {
        Models.IMessageElement Create(ElementTypes.IElement element);
    }
}