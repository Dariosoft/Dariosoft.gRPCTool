using System.Reflection;

namespace Dariosoft.gRPCTool.Factories.ElementNameBuilders
{
    class ProcedureNameBuilder: INameBuildStrategy
    {
        public Enums.ElementType ResponsibleFor { get; } = Enums.ElementType.Procedure;

        public Models.ItemName Create(MemberInfo member)
        {
            var serviceName = GetFromAttribute(member);

            if (string.IsNullOrWhiteSpace(serviceName))
                serviceName = GetFromMethodName(member);

            return new Models.ItemName(Name: serviceName, ProtobufName: serviceName);
        }
        
        private string? GetFromAttribute(MemberInfo procedure)
        {
            var dispalyNameAttribute = procedure.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
            var name = (dispalyNameAttribute?.DisplayName ?? "").AsNameIdentifier();
            return string.IsNullOrWhiteSpace(name) ? null : name;
        }

        private string GetFromMethodName(MemberInfo procedure)
            => procedure.Name.AsNameIdentifier();
    }
}