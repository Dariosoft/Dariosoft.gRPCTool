using System.Reflection;

namespace Dariosoft.gRPCTool.Providers
{
    public interface IProcedureParametersProvider
    {
        IEnumerable<Models.XParameterInfo> Provide(MethodInfo method);
    }

    class ProcedureParametersProvider(Factories.IXTypeFactory xTypeFactory) : IProcedureParametersProvider
    {
        public IEnumerable<Models.XParameterInfo> Provide(MethodInfo method)
        {
            return method.GetParameters()
                .Where(p => !p.IsOut)
                .OrderBy(p => p.Position)
                .Select(p => new { Parameter = p, XType = xTypeFactory.Create(p.ParameterType) })
                .Where(p => !p.XType.IsVoid)
                .Select(p => new Models.XParameterInfo(p.Parameter, p.XType));
        }
    }
}