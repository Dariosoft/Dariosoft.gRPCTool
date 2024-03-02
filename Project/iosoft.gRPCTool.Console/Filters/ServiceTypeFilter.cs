using IServiceTypeFilter = Dariosoft.gRPCTool.Filters.IServiceTypeFilter;

namespace iosoft.gRPCTool.Console.Filters
{
    public class ServiceTypeFilter: IServiceTypeFilter
    {
        public bool Enabled { get; } = true;

        public int Order { get; } = 1;

        public bool Filter(Type serviceType)
        {
            return serviceType.IsInterface && serviceType.GetInterfaces().Any(p => p.Name == "IEndPoint");
        }
    }
}