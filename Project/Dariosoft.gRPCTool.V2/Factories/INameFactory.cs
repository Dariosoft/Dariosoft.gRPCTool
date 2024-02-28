using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface INameFactory
    {
        Models.NameModel Create(Elements.Element element);
    }

    class NameFactory : INameFactory
    {
        private readonly ElementNameStrategies.INameGenerateStrategy[] _strategies;

        public NameFactory(IEnumerable<ElementNameStrategies.INameGenerateStrategy> strategies)
        {
            _strategies = strategies.Where(e => e.Enabled)
                .OrderBy(e => e.Target)
                .ThenBy(e => e.Priority)
                .ToArray();
        }

        public Models.NameModel Create(Elements.Element element)
        {
            var strategy = _strategies.FirstOrDefault(e => e.Target == element.Type);
            return strategy is null
                ? new Models.NameModel(element.Target.Name, $"Grpc{element.Target.Name}")
                : strategy.Create(element);
        }
    }
}