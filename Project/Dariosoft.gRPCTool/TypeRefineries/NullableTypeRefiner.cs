namespace Dariosoft.gRPCTool.TypeRefineries
{
    public class NullableTypeRefiner : ITypeRefiner
    {
        public bool Enabled { get; } = true;

        public int Order { get; } = 2;

        public Type Refine(Type input)
        {
            return input.Name is "Nullable`1"
                ? input.GenericTypeArguments[0]
                : input;
        }

        public override string ToString() => nameof(NullableTypeRefiner);
    }

  
}