namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public class DateMessageNameStrategy : NameGenerateStrategy<Type>
    {
        public DateMessageNameStrategy()
        {
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType ElementType { get; } = Enums.ElementType.Message;

        protected override Models.NameModel Create(Elements.Element element, Type target)
        {
            if (Utilities.DariosoftProtobuf.ValueMessages.TryGetValue(target, out var name))
            {
                return new Models.NameModel(name, $"Grpc{name}");
            }
            
            name = target.GetWellFormedName().AsNameIdentifier();
            return new Models.NameModel(name, $"Grpc{name}");
        }
    }
}