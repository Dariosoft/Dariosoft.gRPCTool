namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class SimpleMessageBuildStrategy(Providers.IProtobufTypeProvider protobufTypeProvider, INameFactory nameFactory) : IMessageBuildStrategy
    {
        public bool IsResponsible(ElementTypes.IElement element)
        {
            var msg = element as ElementTypes.MessageElement;
            return element.Type is Enums.ElementType.Message && (msg is not null && !(
                msg.Descriptor.IsArray ||
                msg.Descriptor.IsDictionary ||
                msg.Descriptor.IsComplex ||
                msg.Descriptor.IsComplexStruct ||
                msg.Descriptor.IsEnum ||
                msg.Descriptor.IsBuffer ||
                msg.Descriptor.IsGuid
                ));
        }
        
        public Models.IMessageElement Create(ElementTypes.IElement element)
        {
            var message = (element as ElementTypes.MessageElement)!;
            var name = nameFactory.Create(message);
            var typeInfo = protobufTypeProvider.Provide(message.Descriptor);
            
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
                        OneOf = typeInfo.OneOf ? [new Models.MessageMemberOneOf { DataType = typeInfo.TypeName, Index = 1, Name = "value" }] : []
                    }
                ]
            };
        }
        
        /*
        public bool IsResponsible(Models.TypeDescriptor descriptor) => !(
            descriptor.IsArray || 
            descriptor.IsDictionary || 
            descriptor.IsComplex || 
            descriptor.IsComplexStruct ||
            descriptor.IsEnum ||
            descriptor.IsBuffer ||
            descriptor.IsGuid
            );
        
        public Models.IMessageElement Create(Models.TypeDescriptor messageTypeDescriptor)
        {
            var name = Protobuf.CustomProtobuf.GetPrimitiveValueMessage(messageTypeDescriptor);
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
                        OneOf = typeInfo.OneOf ? [new Models.MessageMemberOneOf { DataType = typeInfo.TypeName, Index = 1, Name = "value" }] : []
                    }
                ]
            };
        }*/
    }
}