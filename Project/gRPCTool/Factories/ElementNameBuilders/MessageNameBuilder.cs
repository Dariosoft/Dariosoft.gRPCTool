using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.ElementNameBuilders
{
    class MessageNameBuilder : INameBuildStrategy
    {
        public Enums.ElementType ResponsibleFor { get; } = Enums.ElementType.Message;

        public Models.ItemName Create(MemberInfo member)
        {
            var name = (member as Type).GetWellFormedName().AsNameIdentifier();
            return new Models.ItemName(Name: name, ProtobufName: $"Grpc{name}");
        }
    }

   /* class ValueMessageNameBuilder : INameBuildStrategy
    {
        public Enums.ElementType ResponsibleFor { get; } = Enums.ElementType.RpcRequestMessage;
        
        public Models.ItemName Create(MemberInfo member)
        {
            
        }
    }*/
}