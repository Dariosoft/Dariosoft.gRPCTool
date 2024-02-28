using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Components
{
    public class ProtobufProcedureRequestMessageModel: IComponent
    {
        public string Id => Name.ProtobufName;
        
        public required Elements.RequestMessageElement Source { 
            get => _source;
            init
            {
                _source = value;
                Parameters = value is null ? [] : value.MethodInfo.GetParameters();
            }
        }
        
        public required Models.NameModel Name { get; init; }

        public bool IsEmptyMessage => Parameters.Length == 0;
        public ParameterInfo[] Parameters { get; private init; } = [];
        
        public override string ToString() => Name.ProtobufName;
        
        private readonly Elements.RequestMessageElement _source = null!;
    }
}