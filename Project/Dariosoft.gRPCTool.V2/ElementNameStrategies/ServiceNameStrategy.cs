using System.Reflection;

namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public class ServiceNameStrategy : NameGenerateStrategy<Type>
    {
        public ServiceNameStrategy()
        {
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType Target { get; } = Enums.ElementType.Service;

        protected override Models.NameModel Create(Elements.Element element, Type target)
            => GetServiceName(target);

        protected Models.NameModel GetServiceName(Type serviceType)
        {
            var name = GetFromAttribute(serviceType);

            if (string.IsNullOrWhiteSpace(name))
                name = GetFromTypeName(serviceType);

            return new Models.NameModel(name, $"Grpc{name}");
        }

        protected string GetFromTypeName(Type serviceType)
        {
            return serviceType.IsInterface && serviceType.Name.StartsWith('I')
                ? serviceType.Name[1..]
                : serviceType.Name;
        }

        protected string? GetFromAttribute(Type serviceType)
        {
            var dispalyNameAttribute = serviceType.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
            var name = (dispalyNameAttribute?.DisplayName ?? "").AsNameIdentifier();
            return string.IsNullOrWhiteSpace(name) ? null : name;
        }
    }
}