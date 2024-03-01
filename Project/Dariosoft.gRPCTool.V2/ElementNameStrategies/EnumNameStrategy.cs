namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public class EnumNameStrategy : NameGenerateStrategy<Type>
    {
        public EnumNameStrategy()
        {
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType ElementType { get; } = Enums.ElementType.Enum;

        protected override Models.NameModel Create(Elements.Element element, Type target)
        {
            return new Models.NameModel(target.Name, $"Grpc{target.Name}");
        }
    }
}