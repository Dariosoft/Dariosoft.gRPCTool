using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.MessageBuilders
{
    class ComplexMessageBuildStrategy(IMessageMemberFactory memberFactory) : IMessageBuildStrategy
    {
        public bool IsResponsible(Models.TypeDescriptor descriptor) => descriptor.IsComplex || descriptor.IsComplexStruct;

        public Models.IMessageElement Create(Models.TypeDescriptor messageTypeDescriptor)
        {
            var name = messageTypeDescriptor.WellFormedName.AsNameIdentifier();

            return new Models.ComplexMessage
            {
                Name = new Models.ItemName(Name: name, ProtobufName: $"Grpc{name}"),
                Members = GetProperties(messageTypeDescriptor.Type).Select((prop, i) => memberFactory.Create(i + 1, prop)).ToArray()
            };
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