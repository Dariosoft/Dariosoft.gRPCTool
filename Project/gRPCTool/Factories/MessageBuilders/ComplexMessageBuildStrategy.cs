using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class ComplexMessageBuildStrategy(IMessageMemberFactory memberFactory, INameFactory nameFactory) : IMessageBuildStrategy
    {
        public bool IsResponsible(ElementTypes.IElement element)
        {
            var msg = element as ElementTypes.MessageElement;
            return element.Type is Enums.ElementType.Message && (msg is not null && (msg.Descriptor.IsComplex));
        }
        
        public Models.IMessageElement Create(ElementTypes.IElement element)
        {
            var message = (element as ElementTypes.MessageElement)!;
            var name = nameFactory.Create(message);
            
            return new Models.ComplexMessage(message)
            {
                Name = name,
                Members = GetProperties(message.Descriptor.Type).Select((prop, i) => memberFactory.Create(i + 1, prop)).ToArray()
            };
        }
        
        /*public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsComplex || descriptor.IsComplexStruct;

        public Models.IMessageElement Create(Models.TypeDescriptor messageTypeDescriptor)
        {
            var name = messageTypeDescriptor.WellFormedName.AsNameIdentifier();

            return new Models.ComplexMessage
            {
                Name = new Models.ItemName(Name: name, ProtobufName: $"Grpc{name}"),
                Members = GetProperties(messageTypeDescriptor.Type).Select((prop, i) => memberFactory.Create(i + 1, prop)).ToArray()
            };
        }*/
        
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