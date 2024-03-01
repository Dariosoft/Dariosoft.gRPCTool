using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Composers
{
    public class ProtobufProcedureComponentComposer(
        Factories.INameFactory nameFactory,
        Factories.IXTypeFactory xTypeFactory,
        IEnumerable<Filters.IProcedureFilter> filters,
        ProtobufMessageComponentComposer next) : ComponentComposer(next)
    {
        private readonly Filters.IProcedureFilter[] _filters = filters.Where(e => e.Enabled).OrderBy(e => e.Order).ToArray();

        protected override void Process(Components.ProtobufComponent component)
        {
            Components.ProtobufServiceComponent service;

            for (var i = 0; i < component.Services.Count; i++)
            {
                service = component.Services.ElementAt(i);

                var procedures = GetMethods(service.Source)
                    .Where(m => _filters.All(f => f.Filter(m)))
                    .Select(m => new Elements.ProcedureElement(m))
                    .Select(e =>
                    {
                        var reqMessageElement = new Elements.RequestMessageElement(e.MethodInfo);
                        var replyMessageElement = Elements.MessageElement.ReplyMessage(xTypeFactory.Create(e.MethodInfo.ReturnType));

                        return new Components.ProtobufProcedureComponent
                        {
                            Source = e,
                            Name = nameFactory.Create(e),
                            RequestMessage = new Components.ProtobufProcedureRequestMessageComponent
                            {
                                Name = reqMessageElement.HasParameter() ? nameFactory.Create(reqMessageElement) : nameFactory.GoogleEmptyMessage(),
                                Source = reqMessageElement,
                            },
                            ReplyMessage = new Components.ProtobufMessageComponent
                            {
                                Name = replyMessageElement.MessageType.IsVoid ? nameFactory.GoogleEmptyMessage() : nameFactory.Create(replyMessageElement),
                                Source = replyMessageElement,
                            }
                        };
                    });

                service.Procedures.AddRange(procedures);
            }
        }

        private MethodInfo[] GetMethods(Elements.ServiceElement service)
        {
            var serviceType = (service.Target as Type)!;

            var methodQuery = serviceType.IsInterface
                ? serviceType.GetInterfaces().Concat([serviceType]).SelectMany(ExtractMethods)
                : ExtractMethods(serviceType);

            methodQuery = methodQuery.Where(m => !m.IsSpecialName && m.DeclaringType != typeof(object)).ToArray();

            if (_filters.Length > 0)
                methodQuery = methodQuery.Where(m => _filters.All(f => f.Filter(m))).ToArray();

            return methodQuery.ToArray();
        }

        private MethodInfo[] ExtractMethods(Type serviceType)
            => serviceType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    }
}