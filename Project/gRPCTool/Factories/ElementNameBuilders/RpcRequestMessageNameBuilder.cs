using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.ElementNameBuilders
{
    class RpcRequestMessageNameBuilder : INameBuildStrategy
    {
        public Enums.ElementType ResponsibleFor { get; } = Enums.ElementType.RpcRequestMessage;

        public Models.ItemName Create(MemberInfo member)
        {
            var serviceName =  $"Grpc{member.DeclaringType!.Name}";

            if (string.IsNullOrWhiteSpace(serviceName))
                serviceName = $"Grpc{member.DeclaringType!.Name}";

            var name = $"{serviceName}_{member.Name}Req";

            return new Models.ItemName(name, name);
        }
    }
}