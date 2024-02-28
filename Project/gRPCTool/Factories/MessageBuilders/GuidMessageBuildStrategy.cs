namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class GuidMessageBuildStrategy(INameFactory nameFactory) : IMessageBuildStrategy
    {
        public bool IsResponsible(ElementTypes.IElement element)
        {
            var msg = element as ElementTypes.MessageElement;
            return element.Type is Enums.ElementType.Message && (msg is not null && (msg.Descriptor.IsGuid));
        }
        
        public Models.IMessageElement Create(ElementTypes.IElement element)
        {
            var message = (element as ElementTypes.MessageElement)!;
            var name = nameFactory.Create(message);
            
            return new Models.ComplexMessage(message)
            {
                Name = name,
                Members =
                [
                    new Models.MessageMember
                    {
                        Index = 1,
                        DataType = "string",
                        Name = "Value",
                        OneOf = []
                    }
                ]
            };
        }
        
        /*public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsGuid;
        
        public Models.IMessageElement Create(Models.TypeDescriptor messageTypeDescriptor)
        {
            return new Models.ComplexMessage
            {
                Name = new Models.ItemName(Name: Protobuf.CustomProtobuf.GuidValueMessage, ProtobufName: Protobuf.CustomProtobuf.GuidValueMessage),
                Members =
                [
                    new Models.MessageMember
                    {
                        Index = 1,
                        DataType = "string",
                        Name = "Value",
                        OneOf = []
                    }
                ]
            };
        }*/
    }
}