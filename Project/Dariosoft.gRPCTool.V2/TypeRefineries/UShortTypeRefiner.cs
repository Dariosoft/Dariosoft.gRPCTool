using Dariosoft.gRPCTool.V2.CustomTypes;

namespace Dariosoft.gRPCTool.V2.TypeRefineries
{
    public class UShortTypeRefiner : ITypeRefiner
    {
        public bool Enabled { get; } = true;

        public int Order { get; } = 5;

        public Type Refine(Type input)
        {
            return input == typeof(ushort)
                ? typeof(UShortValue)
                : input;
        }
    }
}