using System.Reflection;

namespace Dariosoft.gRPCTool.Composers
{
    public class ProtobufProcedureComponentComposer(
        Factories.INameFactory nameFactory,
        Factories.IXTypeFactory xTypeFactory,
        IEnumerable<Filters.IProcedureFilter> filters,
        Providers.IProcedureParametersProvider procedureParametersProvider,
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
                    .Select(e => new Components.ProtobufProcedureComponent
                    {
                        Source = e,
                        Name = nameFactory.Create(e),
                        RequestMessage = GetRequestMessageComponent(e.MethodInfo),
                        ReplyMessage = GetReplyMessage(e.MethodInfo),
                    });

                service.Procedures.AddRange(procedures);
            }
        }

        private Components.ProtobufProcedureRequestMessageComponent? GetRequestMessageComponent(MethodInfo methodInfo)
        {
            var parameters = procedureParametersProvider.Provide(methodInfo).ToArray();
            
            if (parameters.Length == 0)
                return null;

            var reqMessageElement = new Elements.RequestMessageElement(methodInfo, parameters);

            return new Components.ProtobufProcedureRequestMessageComponent
            {
                Name = nameFactory.Create(reqMessageElement),
                Source = reqMessageElement,
            };
        }

        private Components.ProtobufMessageComponent? GetReplyMessage(MethodInfo methodInfo)
        {
            var type = xTypeFactory.Create(methodInfo.ReturnType);
            if (type.IsVoid) return null;

            var replyMessageElement = Elements.MessageElement.ReplyMessage(type);

            return new Components.ProtobufMessageComponent
            {
                Name = nameFactory.Create(replyMessageElement),
                Source = replyMessageElement,
            };
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