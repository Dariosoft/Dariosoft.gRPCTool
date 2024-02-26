namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class BufferMessageBuildStrategy: IMessageBuildStrategy
    {
        public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsBuffer || descriptor.IsStream;

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
        }
    }
}