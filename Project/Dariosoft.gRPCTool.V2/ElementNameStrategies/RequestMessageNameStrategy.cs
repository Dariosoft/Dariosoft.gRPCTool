using System.Reflection;

namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public class RequestMessageNameStrategy : NameGenerateStrategy<MethodInfo>
    {
        public RequestMessageNameStrategy()
        {
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType Target { get; } = Enums.ElementType.RequestMessage;

        protected override Models.NameModel Create(Elements.Element element, MethodInfo target)
        {
            var name = $"{target.DeclaringType!.Name}_{target.Name}_RequestMessage";
             
            return new Models.NameModel(name, $"Grpc{name}");
        }
    }
}