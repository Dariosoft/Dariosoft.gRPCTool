using System.Reflection;
using Dariosoft.gRPCTool.Providers;
using Dariosoft.gRPCTool.Utilities;

namespace Dariosoft.gRPCTool.MessageCreationStrategies
{
    class DataMessageStrategy(
        Factories.INameFactory nameFactory,
        IProtobufDataTypeProvider dataTypeProvider,
        Factories.IXTypeFactory xTypeFactory
    ) : MessageCreationStrategy
    {
        public Components.ProtobufMessageComponent Create(Elements.MessageElement element, EnqueueElement enqueue)
        {
            var component = new Components.ProtobufMessageComponent
            {
                Name = nameFactory.Create(element),
                Source = element,
            };
      
            var props = GetProperties(element.MessageType.Type).ToArray();

            for (var i = 0; i < props.Length; i++)
            {
                var dataType = dataTypeProvider.Provide(xTypeFactory.Create(props[i].PropertyType), enqueue);
                component.Members.Add(
                    new Components.ProtobufMessageMemberComponent
                    {
                        Index = i + 1,
                        Name = new Models.NameModel(props[i].Name.ToPublicCase()),
                        DataType = dataType.TypeName,
                        OneOf = dataType.Oneof
                            ?
                            [
                                new Components.MessageMemberOneOf
                                {
                                    Index = i + 1,
                                    Name = props[i].Name.ToPrivateCase(),
                                    DataType = dataType.TypeName
                                }
                            ]
                            : []
                    });
            }

            return component;
        }

        private IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.IsInterface
                ? type.GetInterfaces()
                    .SelectMany(i => i.GetProperties())
                    .Concat(type.GetProperties())
                : type.GetProperties();
        }
    }
}