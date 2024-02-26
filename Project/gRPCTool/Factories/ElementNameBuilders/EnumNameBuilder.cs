using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.ElementNameBuilders
{
    class EnumNameBuilder : INameBuildStrategy
    {
        public Enums.ElementType ResponsibleFor { get; } = Enums.ElementType.Enum;

        public Models.ItemName Create(MemberInfo member)
        {
            var name = (member as Type).GetWellFormedName().AsNameIdentifier();
            return new Models.ItemName(Name: name, ProtobufName: $"Grpc{name}");
        }
    }
}