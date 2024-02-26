namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class GuidMessageBuildStrategy : IMessageBuildStrategy
    {
        public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsGuid;
        
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
        }
    }
}