using Dariosoft.gRPCTool.TypeRefineries;

namespace iosoft.gRPCTool.Console.TypeRefineries
{
    class RequestRefiner : ITypeRefiner
    {
        public bool Enabled { get; } = true;
        public int Order { get; } = 1;

        public Type Refine(Type input)
        {
            if(input.IsAssignableTo(typeof(Dariosoft.Framework.IRequest)))
            {
                input = input.GenericTypeArguments.Length > 0 
                    ? input.GenericTypeArguments[0] 
                    : typeof(void);
            }
        
            return input;
        }
    }
}