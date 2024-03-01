
namespace Dariosoft.gRPCTool.V2.Composers
{
    public class ProtobufMessageComponentComposer(Factories.IProtobufMessageComponentFactory messageComponentFactory) : ComponentComposer(null)
    {
        private readonly Queue<Elements.Element> _queue = new();

        private void Enqueue(Elements.Element element)
        {
            
            if (!_queue.Any(i => i.Key == element.Key))
                _queue.Enqueue(element);
        }

        protected override void Process(Components.ProtobufComponent component)
        {
            for (var i = 0; i < component.Services.Count; i++)
            {
                var service = component.Services.ElementAt(i);
                for (var p = 0; p < service.Procedures.Count; p++)
                {
                    var procedure = service.Procedures.ElementAt(p);

                    if (procedure.RequestMessage.Source.HasParameter())
                        component.Messages.Add(CreateMessageComponent(procedure.RequestMessage.Source));

                    if (!(procedure.ReplyMessage.Source as Elements.MessageElement)!.MessageType.IsVoid)
                        component.Messages.Add(CreateMessageComponent(procedure.ReplyMessage.Source));
                }
            }

            Flush(component);
        }

        private void Flush(Components.ProtobufComponent component)
        {
            Elements.Element? item = null;

            while (_queue.TryDequeue(out item))
            {
                if (item.Type == Enums.ElementType.Enum)
                    component.Enums.Add(CreateEnumComponent(item));
                else
                    component.Messages.Add(CreateMessageComponent(item));
            }
        }

        private Components.ProtobufEnumComponent CreateEnumComponent(Elements.Element element)
            => (messageComponentFactory.Create(element, Enqueue) as Components.ProtobufEnumComponent)!;
        
        private Components.ProtobufMessageComponent CreateMessageComponent(Elements.Element element)
            => (messageComponentFactory.Create(element, Enqueue) as Components.ProtobufMessageComponent)!;
    }
}