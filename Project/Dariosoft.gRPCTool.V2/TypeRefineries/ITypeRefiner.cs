namespace Dariosoft.gRPCTool.V2.TypeRefineries
{
    public interface ITypeRefiner
    {
        bool Enabled { get; }

        int Order { get; }

        Type Refine(Type input);
    }
}