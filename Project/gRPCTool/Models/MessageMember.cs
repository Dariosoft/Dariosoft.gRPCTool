namespace Dariosoft.gRPCTool.Models
{
    public record MessageMember
    {
        public required int Index { get; init; }

        public required string Name { get; init; } = "";

        public required string DataType { get; init; } = "";

        public MessageMemberOneOf[] OneOf { get; init; } = [];

        public override string ToString() => Name;
    }

    public record MessageMemberOneOf
    {
        public required int Index { get; init; }

        public required string Name { get; init; } = "";

        public required string DataType { get; init; } = "";
    }
}
