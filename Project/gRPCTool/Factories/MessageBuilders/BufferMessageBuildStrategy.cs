namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    /*class BufferMessageBuildStrategy(Providers.IProtobufTypeProvider protobufTypeProvider, INameFactory nameFactory) : IMessageBuildStrategy
    {
        public bool IsResponsible(ElementTypes.IElement element) => element.Type == Enums.ElementType.Procedure;

        public Models.IMessageElement Create(ElementTypes.IElement element)
        {
            
        }
    }*/
    
    class BufferMessageBuildStrategy(Providers.IProtobufTypeProvider protobufTypeProvider, INameFactory nameFactory): IMessageBuildStrategy
    {
        public bool IsResponsible(ElementTypes.IElement element)
        {
            var msg = element as ElementTypes.MessageElement;
            return element.Type is Enums.ElementType.Message && (msg is not null && (msg.Descriptor.IsBuffer || msg.Descriptor.IsStream));
        }
        
        public Models.IMessageElement Create(ElementTypes.IElement element)
        {
            var message = (element as ElementTypes.MessageElement)!;
            var name = nameFactory.Create(message);
            var typeInfo = protobufTypeProvider.Provide(message.Source);
            
            return new Models.ComplexMessage(message)
            {
                Name = name,
                Members =
                [
                    new Models.MessageMember
                    {
                        Index = 1,
                        DataType = typeInfo.TypeName,
                        Name = "Value",
                        OneOf = []
                    }
                ]
            };
        }
        
        /*public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsBuffer || descriptor.IsStream;

        public Models.IMessageElement Create(Models.TypeDescriptor messageTypeDescriptor)
        {
            return new Models.ComplexMessage
            {
                Name = new Models.ItemName(Name: Protobuf.CustomProtobuf.BytesMessage, ProtobufName: Protobuf.CustomProtobuf.BytesMessage),
                Members =
                [
                    new Models.MessageMember
                    {
                        Index = 1,
                        DataType = Protobuf.GoogleProtobuf.Bytes,
                        Name = "Value",
                        OneOf = []
                    }
                ]
            };
        }*/
    }
}