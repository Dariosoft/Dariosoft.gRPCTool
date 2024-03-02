namespace Dariosoft.gRPCTool.Composers
{
    public class ProtobufServiceComponentComposer(
        Factories.INameFactory nameFactory,
        IEnumerable<Filters.IServiceTypeFilter> filters,
        ProtobufProcedureComponentComposer next) : ComponentComposer(next)
    {
        private readonly Filters.IServiceTypeFilter[] _filters = filters.Where(e => e.Enabled).OrderBy(e => e.Order).ToArray();

        protected override void Process(Components.ProtobufComponent component)
        {
            var services = component.Source.GetTypes()
                .Where(t => _filters.All(f => f.Filter(t)))
                .Select(t => new Elements.ServiceElement(t))
                .Select(e => new Components.ProtobufServiceComponent
                {
                    Name = nameFactory.Create(e),
                    Source = e
                });

            component.Services.AddRange(services);
        }
    }
}