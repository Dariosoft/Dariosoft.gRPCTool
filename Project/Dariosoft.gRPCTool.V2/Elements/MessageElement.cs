namespace Dariosoft.gRPCTool.V2.Elements
{
    public class MessageElement : Element
    {
        private MessageElement(Models.XType target, Enums.ElementType type)
            : base(target.Type, type)
        {
            MessageType = target;
        }

        public Models.XType MessageType { get; }
        
        public static MessageElement ReplyMessage(Models.XType target) => new MessageElement(target, Enums.ElementType.ReplyMessage);

        public static MessageElement DataMessage(Models.XType target) => new MessageElement(target, Enums.ElementType.Message);
    }
}