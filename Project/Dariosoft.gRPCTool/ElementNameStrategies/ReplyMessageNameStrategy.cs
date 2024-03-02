namespace Dariosoft.gRPCTool.ElementNameStrategies
{
    public class ReplyMessageNameStrategy : NameGenerateStrategy<Type>
    {
        private readonly Factories.IXTypeFactory _xTypeFactory;

        public ReplyMessageNameStrategy(Factories.IXTypeFactory xTypeFactory)
        {
            this.Enabled = true;
            this.Priority = 1;
            _xTypeFactory = xTypeFactory;
        }

        public override Enums.ElementType ElementType { get; } = Enums.ElementType.ReplyMessage;

        protected override Models.NameModel Create(Elements.Element element, Type target)
        {
            var xType = _xTypeFactory.Create(target);
            if (!Utilities.DariosoftProtobuf.ValueMessages.TryGetValue(xType.Type, out var name))
                name = xType.WellFormedName.AsNameIdentifier();

            if (xType.IsArray)
            {
            }
            else if (xType.IsBuffer || xType.IsStream)
            {
                name = "ByteArray"; //name.TrimEnd(' ', '_')
            }

            return new Models.NameModel(name, $"Grpc{name}");
        }
    }
}