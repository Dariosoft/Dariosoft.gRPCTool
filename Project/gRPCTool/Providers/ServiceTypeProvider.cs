using System.Reflection;

namespace Dariosoft.gRPCTool.Providers
{
    public interface IServiceTypeProvider
    {
        ElementTypes.ServiceElement[] GetServiceTypes(Assembly assembly);
    }

    internal class ServiceTypeProvider : IServiceTypeProvider
    {
        private readonly Filters.IServiceTypeFilter[] _filters;

        public ServiceTypeProvider(IEnumerable<Filters.IServiceTypeFilter> filters)
        {
            _filters = filters
                .Where(e => e.Enabled)
                .OrderBy(x => x.Order)
                .ToArray();
        }

        public ElementTypes.ServiceElement[] GetServiceTypes(Assembly assembly)
        {
            return assembly
                .GetExportedTypes()
                .Where(t => _filters.All(e => e.Filter(t)))
                .Select(t => new ElementTypes.ServiceElement(t))
                .ToArray();
        }
    }
}