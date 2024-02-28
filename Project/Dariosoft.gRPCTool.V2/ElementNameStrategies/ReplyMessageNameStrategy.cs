namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public class ReplyMessageNameStrategy : NameGenerateStrategy<Type>
    {
        public ReplyMessageNameStrategy()
        {
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType Target { get; } = Enums.ElementType.ReplyMessage;

        protected override Models.NameModel Create(Elements.Element element, Type target)
        {
            var name = (target.FullName ?? target.Name).AsNameIdentifier();
             
            return new Models.NameModel(name, $"Grpc{name}");
        }
    }
}