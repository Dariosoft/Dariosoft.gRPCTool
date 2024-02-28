using Filters = Dariosoft.gRPCTool.V2.Filters;

namespace Dariosoft.TestConsole.V2
{
    public class ServiceTypeFilter: Filters.IServiceTypeFilter
    {
        public bool Enabled { get; } = true;

        public int Order { get; } = 1;

        public bool Filter(Type serviceType)
        {
            return serviceType.IsInterface && serviceType.GetInterfaces().Any(p => p.Name == "IEndPoint");
        }
    }
}