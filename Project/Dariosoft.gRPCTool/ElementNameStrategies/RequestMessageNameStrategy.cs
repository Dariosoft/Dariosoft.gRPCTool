using System.Reflection;

namespace Dariosoft.gRPCTool.ElementNameStrategies
{
    public class RequestMessageNameStrategy : NameGenerateStrategy<MethodInfo>
    {
        private readonly ServiceNameStrategy _serviceNameStrategy;

        public RequestMessageNameStrategy(ServiceNameStrategy serviceNameStrategy)
        {
            _serviceNameStrategy = serviceNameStrategy;
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType ElementType { get; } = Enums.ElementType.RequestMessage;

        protected override Models.NameModel Create(Elements.Element element, MethodInfo target)
        {
            var svcName = _serviceNameStrategy.GetServiceName(target.DeclaringType!).Name;
            var name = $"{svcName}_{target.Name}_RequestMessage";
             
            return new Models.NameModel(name, $"Grpc{name}");
        }
    }
}