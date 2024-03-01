namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufMessageMemberComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        public required Models.NameModel Name { get; init; }
        
        public required int Index { get; init; }

        public required string DataType { get; init; } = "";

        public MessageMemberOneOf[] OneOf { get; init; } = [];
        
        public override string ToString() => Name.ProtobufName;
    }
}