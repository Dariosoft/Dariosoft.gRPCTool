using Dariosoft.gRPCTool.CustomTypes;

namespace Dariosoft.gRPCTool.TypeRefineries
{
    public class DecimalTypeRefiner : ITypeRefiner
    {
        public bool Enabled { get; } = true;

        public int Order { get; } = 5;

        public Type Refine(Type input)
        {
            return input == typeof(decimal)
                ? typeof(DecimalValue)
                : input;
        }
    }
}