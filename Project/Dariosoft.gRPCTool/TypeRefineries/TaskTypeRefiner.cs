namespace Dariosoft.gRPCTool.TypeRefineries
{
    public class TaskTypeRefiner : ITypeRefiner
    {
        public bool Enabled { get; } = true;

        public int Order { get; } = 1;

        public Type Refine(Type input)
        {
            return (input.Name is "Task`1" or "ValueTask`1")
                ? input.GenericTypeArguments[0]
                : input;
        }

        public override string ToString() => nameof(TaskTypeRefiner);
    }
}