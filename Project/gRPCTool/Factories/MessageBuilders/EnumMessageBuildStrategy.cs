namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class EnumMessageBuildStrategy(Providers.IProtobufTypeProvider protobufTypeProvider): IMessageBuildStrategy
    {
        public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsEnum;

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
        }
        
    }
}