namespace Dariosoft.gRPCTool.Models
{
    public record EmptyMessage : IMessageElement
    {
        public EmptyMessage()
        {
            //this.Element = new ElementTypes.MessageElement();
        }

        public ElementTypes.IElement Element { get; } = null!;
        
        public string HashKey => gRPCTool.Protobuf.GoogleProtobuf.EmptyMessage;

        public ItemName Name { get; } = new ItemName(Name: "Empty", ProtobufName: gRPCTool.Protobuf.GoogleProtobuf.EmptyMessage);
    }
}
