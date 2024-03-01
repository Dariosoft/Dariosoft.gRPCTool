using Dariosoft.gRPCTool.V2.Models;

namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufProcedureComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        
        public required Elements.ProcedureElement Source { get; init; }
        
        public required NameModel Name { get; init; }
        
        public required ProtobufProcedureRequestMessageComponent RequestMessage { get; init; }
        public required ProtobufMessageComponent ReplyMessage { get; init; }
        
      //  public required Elements.RequestMessageElement RequestMessage { get; init; }
        
        //public required Elements.MessageElement ReplyMessage { get; init; }
        
        public override string ToString() => Name.ProtobufName;
    }
}