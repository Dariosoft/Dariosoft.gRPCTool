namespace Dariosoft.gRPCTool.V2.Filters
{
    public interface IServiceTypeFilter
    {
        bool Enabled { get; }

        int Order { get; }

        bool Filter(Type serviceType);
    }
    
}