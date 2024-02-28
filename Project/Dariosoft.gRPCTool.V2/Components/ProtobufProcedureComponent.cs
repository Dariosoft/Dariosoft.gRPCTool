using System.Reflection;
using Dariosoft.gRPCTool.V2.Models;

namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufProcedureComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        
        public required Elements.ProcedureElement Source { get; init; }
        public required NameModel Name { get; init; }
        
        public required ProtobufProcedureRequestMessageModel RequestMessage { get; init; }
        
        public required ProtobufMessageComponent ReplyMessage { get; init; }
        
        public override string ToString() => Name.ProtobufName;
    }
}