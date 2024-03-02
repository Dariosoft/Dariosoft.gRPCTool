using System.Reflection;

namespace Dariosoft.gRPCTool.Filters
{
    public interface IProcedureFilter
    {
        bool Enabled { get; }

        int Order { get; }

        bool Filter(MethodInfo info);
    }
}