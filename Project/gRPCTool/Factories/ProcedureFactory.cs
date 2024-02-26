namespace Dariosoft.gRPCTool.Factories
{
    public interface IProcedureFactory
    {
        Models.ProtobufProcedure Create(ElementTypes.ProcedureElement element);
    }

    internal class ProcedureFactory(
        IMessageFactory messageFactory,
        INameFactory nameFactory,
        Accessories.IElementPool pool
    ) : IProcedureFactory
    {
        public Models.ProtobufProcedure Create(ElementTypes.ProcedureElement element)
        {
            var model = new Models.ProtobufProcedure(element)
            {
                Name = nameFactory.Create(element),
                RequestMessage = messageFactory.Create(element.RequestMessageElement),
                ResponseMessage = messageFactory.Create(element.ReplyMessageElement)
            };

            pool.Add(model);

            return model;
        }
    }
}