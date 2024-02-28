namespace Dariosoft.gRPCTool.V2.ElementNameStrategies
{
    public interface INameGenerateStrategy
    {
        bool Enabled { get; }

        int Priority { get; }
        
        Enums.ElementType Target { get; }

        Models.NameModel Create(Elements.Element element);
    }
}