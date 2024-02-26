namespace Dariosoft.gRPCTool.Accessories
{
    public interface IEnumFactoryQueueFeeder : ITypeQFeeder<ElementTypes.EnumElement> { }

    public interface IEnumFactoryQueueReader : ITypeQReader<ElementTypes.EnumElement> { }

    internal class EnumFactoryQueue(Analyzers.ITypeAnalyzer typeAnalyzer) : TypeQueue<ElementTypes.EnumElement>, IEnumFactoryQueueFeeder, IEnumFactoryQueueReader
    {
        public override void Enqueue(ElementTypes.EnumElement element)
        {
            if (!typeAnalyzer.Analyze(element.source).IsEnum)
                throw new ArgumentException($"{element.source.FullName ?? element.source.Name} is not an enum.");

            base.Enqueue(element);
        }
    }


}
