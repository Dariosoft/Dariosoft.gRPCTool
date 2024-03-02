using Dariosoft.gRPCTool.Models;

namespace Dariosoft.gRPCTool.Components
{
    public class ProtobufServiceComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        
        public required Elements.ServiceElement Source { get; init; }
        public required NameModel Name { get; init; }
        
        public Utilities.ComponentList<ProtobufProcedureComponent> Procedures { get; } = [];

        public override string ToString() => Name.ProtobufName;
    }
}