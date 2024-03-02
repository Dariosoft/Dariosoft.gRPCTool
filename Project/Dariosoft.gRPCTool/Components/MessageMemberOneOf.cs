namespace Dariosoft.gRPCTool.Components
{
    public class MessageMemberOneOf
    {
        public required int Index { get; init; }

        public required string Name { get; init; } = "";

        public required string DataType { get; init; } = "";

        public override string ToString() => Name;
    }
}