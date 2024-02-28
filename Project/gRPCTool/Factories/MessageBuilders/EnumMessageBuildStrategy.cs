namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class EnumMessageBuildStrategy(Providers.IProtobufTypeProvider protobufTypeProvider, INameFactory nameFactory): IMessageBuildStrategy
    {
        public bool IsResponsible(ElementTypes.IElement element)
        {
            var msg = element as ElementTypes.MessageElement;
            return element.Type is Enums.ElementType.Message && (msg is not null && (msg.Descriptor.IsEnum));
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
        
        /*public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsEnum;

        public Models.IMessageElement Create(Models.TypeDescriptor messageTypeDescriptor)
        {
            var name = Protobuf.CustomProtobuf.GetEnumMessage(messageTypeDescriptor.Type);
            var typeInfo = protobufTypeProvider.Provide(messageTypeDescriptor);

            return new Models.ComplexMessage
            {
                Name = new Models.ItemName(Name: name, ProtobufName: name),
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
        }*/
        
    }
}