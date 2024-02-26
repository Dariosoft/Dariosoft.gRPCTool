
namespace Dariosoft.gRPCTool.Models
{
    public interface IProtobufElement
    {
        ElementTypes.IElement Element { get; }
        
        ItemName Name { get; }

        string HashKey { get; }
    }

    public interface IServiceElement : IProtobufElement
    {
    }

    public interface IProcedureElement : IProtobufElement
    {
    }

    public interface IMessageElement : IProtobufElement
    {
    }

    public interface IEnumMessageElement : IProtobufElement
    {
    }
}
