namespace Dariosoft.gRPCTool.Components
{

    
    public class ProtobufMessageComponent : IMessageComponent
    {
        public string Id => Name.ProtobufName;

        public required Models.NameModel Name { get; init; }
        
        public required Elements.Element Source
        {
            get => _source;
            init => _source = IsElligable(value) ? value : throw new NotSupportedException();
        }

        public Utilities.ComponentList<ProtobufMessageMemberComponent> Members { get; } = [];
        
        public override string ToString() => Name.ProtobufName;

        private readonly Elements.Element _source = null!;

        private static bool IsElligable(Elements.Element element)
        {
            return element.Type 
                is Enums.ElementType.ReplyMessage 
                or Enums.ElementType.RequestMessage
                or Enums.ElementType.Message;
        }
    }
}