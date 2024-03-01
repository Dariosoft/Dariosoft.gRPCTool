using System.Reflection;

namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public class ProcedureNameStrategy : NameGenerateStrategy<MethodInfo>
    {
        public ProcedureNameStrategy()
        {
            this.Enabled = true;
            this.Priority = 1;
        }

        public override Enums.ElementType ElementType { get; } = Enums.ElementType.Procedure;

        protected override Models.NameModel Create(Elements.Element element, MethodInfo target)
        {
            return new Models.NameModel(target.Name);
        }
    }
}