using System.Reflection;

namespace Dariosoft.gRPCTool.Factories
{
    public interface IProtobufFactory
    {
        Models.Protobuf Create(Assembly assembly);
    }

    internal class ProtobufFactory(
        IProtobufHeaderFactory headerFactory,
        IServiceFactory serviceFactory,
        IMessageFactory messageFactory,
        IEnumMessageFactory enumMessageFactory,
        Providers.IServiceTypeProvider serviceTypeProvider,
        Accessories.IElementPool pool
        ) : IProtobufFactory
    {
        public Models.Protobuf Create(Assembly assembly)
        {
            var serviceTypes = serviceTypeProvider.GetServiceTypes(assembly);

            var services = serviceTypes.Select(serviceFactory.Create).ToArray();

            messageFactory.Flush();
            enumMessageFactory.Flush();

            return new Models.Protobuf
            {
                Assembly = assembly,
                Header = headerFactory.Create(assembly),
                Services = services,
                Messages = pool.Items.OfType<Models.ComplexMessage>().ToArray(),
                Enums = pool.Items.OfType<Models.EnumMessage>().ToArray()
            };
        }
    }
}
