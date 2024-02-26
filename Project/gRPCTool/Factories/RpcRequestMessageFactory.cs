using System.Reflection;

namespace Dariosoft.gRPCTool.Factories
{
    public interface IRpcRequestMessageFactory
    {
        Models.IMessageElement Create(ElementTypes.RpcRequestMessageElement element);
    }

    class RpcRequestMessageFactory(
        IMessageMemberFactory memberFactory,
        INameFactory nameFactory,
        Accessories.IElementPool pool
        ) : IRpcRequestMessageFactory
    {
        public Models.IMessageElement Create(ElementTypes.RpcRequestMessageElement element)
        {
            if (element.Parameters.Length == 0) return new Models.EmptyMessage();
            
            var model = new Models.ComplexMessage(element)
            {
                Name =  nameFactory.Create(element),
                Members = element.Parameters.Select((param, i) => memberFactory.Create(i + 1, param)).ToArray()
            };

            pool.Add(model);

            return model;
        }
    }
}
