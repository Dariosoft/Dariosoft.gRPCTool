using System.Reflection;

namespace Dariosoft.gRPCTool.Providers
{
    public interface IProcedureMethodProvider
    {
        MethodInfo[] GetMethods(Type serviceType);
    }

    public class ProcedureProvider : IProcedureMethodProvider
    {
        private readonly Filters.IProcedureFilter[] _filters = [];

        public ProcedureProvider(IEnumerable<Filters.IProcedureFilter> procedureFilters)
        {
            _filters = procedureFilters
                .Where(e => e.Enabled)
                .OrderBy(e => e.Order).ToArray();
        }

        public virtual MethodInfo[] GetMethods(Type serviceType)
        {
            var methodQuery = serviceType.IsInterface
               ? serviceType.GetInterfaces().Concat([serviceType]).SelectMany(ExtractMethods)
               : ExtractMethods(serviceType);

            methodQuery = methodQuery.Where(m => !m.IsSpecialName && m.DeclaringType != typeof(object)).ToArray();

            if (_filters.Length > 0)
                methodQuery = methodQuery.Where(m => _filters.All(f => f.Filter(m))).ToArray();

            return methodQuery.ToArray();
        }

        private MethodInfo[] ExtractMethods(Type serviceType)
            => serviceType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    }
}
