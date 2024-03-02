namespace Dariosoft.gRPCTool.Elements
{
    public class ServiceElement : Element
    {
        public ServiceElement(Type serviceType)
            : base(Elligable(serviceType) ? serviceType : throw new ArgumentException("The service type must be a class or an interface.", nameof(serviceType)), Enums.ElementType.Service)
        {
            this.ServiceType = serviceType;
        }

        public Type ServiceType { get; }

        private static bool Elligable(Type serviceType)
        {
            return (serviceType.IsClass || serviceType.IsInterface) &&
                   serviceType != typeof(object) &&
                   serviceType != typeof(void) &&
                   serviceType is { IsEnum: false, IsPrimitive: false };
        }
    }
}