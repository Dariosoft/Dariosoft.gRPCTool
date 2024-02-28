using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IOutputFactory
    {
        Utilities.IOuput Create(Assembly assembly);
    }
}