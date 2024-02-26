namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    public interface IMessageBuildStrategy : IMessageBuilder
    {
        bool IsResponsible(ElementTypes.IElement element);
    }
}