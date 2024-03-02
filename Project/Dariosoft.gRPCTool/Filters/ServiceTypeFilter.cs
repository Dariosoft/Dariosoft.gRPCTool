namespace Dariosoft.gRPCTool.Filters
{
    public interface IServiceTypeFilter
    {
        bool Enabled { get; }

        int Order { get; }

        bool Filter(Type serviceType);
    }
    
}