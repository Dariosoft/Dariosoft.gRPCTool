
namespace Dariosoft.gRPCTool.V2.MessageCreationStrategies
{
    class ReplyMessageStrategy(
        Factories.INameFactory nameFactory, 
        Factories.IProtobufDataTypeFactory dataTypeFactory,
        
        DataMessageStrategy dataMessageStrategy) : MessageCreationStrategy
    {
        public Components.ProtobufMessageComponent Create(Elements.MessageElement element, Delegates.EnqueueElement enqueue)
        {
            if (element.MessageType.IsComplex)
                return dataMessageStrategy.Create(element, enqueue);
            
            var component = new Components.ProtobufMessageComponent
            {
                Name = nameFactory.Create(element),
                Source = element,
            };

            var dataType = dataTypeFactory.Create(element.MessageType, enqueue);
            component.Members.Add(new Components.ProtobufMessageMemberComponent
            {
                Index = 1,
                Name = new Models.NameModel("Value"),
                DataType = dataType.TypeName,
                OneOf = dataType.Oneof ? [new Components.MessageMemberOneOf
                {
                    Index = 1,
                    Name = "value",
                    DataType = dataType.TypeName 
                } ] : []
            });
            
            return component;
        }
    }
}