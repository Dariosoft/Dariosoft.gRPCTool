using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Filters
{
    public interface IProcedureFilter
    {
        bool Enabled { get; }

        int Order { get; }

        bool Filter(MethodInfo info);
    }
}