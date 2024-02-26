namespace Dariosoft.gRPCTool.Factories
{
    public interface IServiceFactory
    {
        Models.ProtobufService Create(ElementTypes.ServiceElement service);
    }

    internal class ServiceFactory(
        Providers.IProcedureMethodProvider procedureProvider,
        IProcedureFactory procedureFactory,
        INameFactory nameFactory,
        IEnumerable<Filters.IProcedureFilter> filters,
        Accessories.IElementPool pool
        ) : IServiceFactory
    {

        private readonly Filters.IProcedureFilter[] _filters = filters.Where(e => e.Enabled).OrderBy(e => e.Order).ToArray();
        
        public Models.ProtobufService Create(ElementTypes.ServiceElement service)
        {
            var model = new Models.ProtobufService(service)
            {
                Name = nameFactory.Create(service),
                Procedures = procedureProvider
                    .GetMethods(service.Source)
                    .Where(m => _filters.All(f => f.Filter(m)))
                    .Select(m => new ElementTypes.ProcedureElement(m))
                    .Select(procedureFactory.Create)
                    .ToArray(),
            };

            pool.Add(model);

            return model;
        }
    }
}
