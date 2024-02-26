using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.ElementNameBuilders
{
    class ServiceNameBuilder : INameBuildStrategy
    {
        public Enums.ElementType ResponsibleFor { get; } = Enums.ElementType.Service;

        public Models.ItemName Create(MemberInfo member)
        {
            var serviceName = GetFromAttribute(member);

            if (string.IsNullOrWhiteSpace(serviceName))
                serviceName = GetFromTypeName((member as Type)!);

            return new Models.ItemName(Name: serviceName, ProtobufName: $"Grpc{serviceName}");
        }

        private string GetFromTypeName(Type serviceType)
        {
            return serviceType.IsInterface && serviceType.Name.StartsWith('I')
                ? serviceType.Name[1..]
                : serviceType.Name;
        }

        private string? GetFromAttribute(MemberInfo serviceType)
        {
            var dispalyNameAttribute = serviceType.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
            var name = (dispalyNameAttribute?.DisplayName ?? "").AsNameIdentifier();
            return string.IsNullOrWhiteSpace(name) ? null : name;
        }
    }
}