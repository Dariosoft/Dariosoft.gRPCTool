using Dariosoft.gRPCTool.Providers;
using Dariosoft.gRPCTool.Utilities;

namespace Dariosoft.gRPCTool.MessageCreationStrategies
{
    class ReplyMessageStrategy(
        Factories.INameFactory nameFactory, 
        IProtobufDataTypeProvider dataTypeProvider,
        
        DataMessageStrategy dataMessageStrategy) : MessageCreationStrategy
    {
        public Components.ProtobufMessageComponent Create(Elements.MessageElement element, EnqueueElement enqueue)
        {
            if (element.MessageType.IsComplex)
                return dataMessageStrategy.Create(element, enqueue);
            
            var dataType = dataTypeProvider.Provide(element.MessageType, enqueue);

            // if (dataType.IsValueMessage)
            // {
            //     
            // }
            
            var component = new Components.ProtobufMessageComponent
            {
                Name = nameFactory.Create(element),
                Source = element,
            };

            
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