using Dariosoft.gRPCTool.V2.Providers;

namespace Dariosoft.gRPCTool.V2.MessageCreationStrategies
{
    class RequestMessageStrategy(
        Factories.INameFactory nameFactory,
        IProtobufDataTypeProvider dataTypeProvider,
        Factories.IXTypeFactory xTypeFactory
    ) : MessageCreationStrategy
    {
        public Components.ProtobufMessageComponent Create(Elements.RequestMessageElement element, Delegates.EnqueueElement enqueue)
        {
            if (element.Parameters.Length == 0)
                throw new ArgumentException("The element has no parameters.", nameof(element));

            var component = new Components.ProtobufMessageComponent
            {
                Name = nameFactory.Create(element),
                Source = element,
            };

            var parameters = element.MethodInfo
                .GetParameters()
                .Where(p => !p.IsOut)
                .ToArray();

            for (var p = 0; p < parameters.Length; p++)
            {
                var dataType = dataTypeProvider.Provide(xTypeFactory.Create(parameters[p].ParameterType), enqueue);

                component.Members.Add(
                    new Components.ProtobufMessageMemberComponent
                    {
                        Index = p + 1,
                        Name = new Models.NameModel(parameters[p].Name!.ToPublicCase()),
                        DataType = dataType.TypeName,
                        OneOf = dataType.Oneof
                            ? [new Components.MessageMemberOneOf { DataType = dataType.TypeName, Index = p + 1, Name = parameters[p].Name!.ToPrivateCase() }]
                            : []
                    });
            }

            return component;
        }
    }
}