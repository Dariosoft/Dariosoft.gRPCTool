namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class SimpleMessageBuildStrategy(Providers.IProtobufTypeProvider protobufTypeProvider) : IMessageBuildStrategy
    {
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
        }
    }
}