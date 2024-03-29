using System.Reflection;

namespace Dariosoft.gRPCTool.Components
{
    public class ProtobufProcedureRequestMessageComponent: IComponent
    {
        public string Id => Name.ProtobufName;
        
        public required Elements.RequestMessageElement Source { 
            get => _source;
            init
            {
                _source = value;
                Parameters = value is null ? [] : value.MethodInfo.GetParameters().Where(p => !p.IsOut).ToArray();
            }
        }
        
        public required Models.NameModel Name { get; init; }

        public ParameterInfo[] Parameters { get; private init; } = [];
        
        public override string ToString() => Name.ProtobufName;
        
        private readonly Elements.RequestMessageElement _source = null!;
    }
}