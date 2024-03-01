using System.Reflection;

namespace Dariosoft.gRPCTool.V2.MessageCreationStrategies
{
    class DataMessageStrategy(
        Factories.INameFactory nameFactory,
        Factories.IProtobufDataTypeFactory dataTypeFactory,
        Factories.IXTypeFactory xTypeFactory
    ) : MessageCreationStrategy
    {
        public Components.ProtobufMessageComponent Create(Elements.MessageElement element, Delegates.EnqueueElement enqueue)
        {
            var component = new Components.ProtobufMessageComponent
            {
                Name = nameFactory.Create(element),
                Source = element,
            };

            var props = GetProperties(element.MessageType.Type)
                // .Select((p, i) => )
                .ToArray();

            for (var i = 0; i < props.Length; i++)
            {
                var dataType = dataTypeFactory.Create(xTypeFactory.Create(props[i].PropertyType), enqueue);
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