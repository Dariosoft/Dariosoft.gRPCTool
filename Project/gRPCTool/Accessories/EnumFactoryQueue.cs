namespace Dariosoft.gRPCTool.Accessories
{
    public interface IEnumFactoryQueueFeeder : ITypeQFeeder<ElementTypes.EnumElement> { }

    public interface IEnumFactoryQueueReader : ITypeQReader<ElementTypes.EnumElement> { }

    internal class EnumFactoryQueue(Analyzers.ITypeAnalyzer typeAnalyzer) : TypeQueue<ElementTypes.EnumElement>, IEnumFactoryQueueFeeder, IEnumFactoryQueueReader
    {
        public override void Enqueue(ElementTypes.EnumElement element)
        {
            if (!typeAnalyzer.Analyze(element.Source).IsEnum)
                throw new ArgumentException($"{element.Source.FullName ?? element.Source.Name} is not an enum.");

            base.Enqueue(element);
        }
    }


}
