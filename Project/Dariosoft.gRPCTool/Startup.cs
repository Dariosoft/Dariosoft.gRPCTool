using Microsoft.Extensions.DependencyInjection;

namespace Dariosoft.gRPCTool
{
    public static class Startup
    {
        public static IServiceCollection AddgRPCTool(this IServiceCollection services, Action<Options> configure)
        {
            var allTypes = typeof(Startup).Assembly.GetTypes();

            return services
                .RegisterOf(typeof(ElementNameStrategies.INameGenerateStrategy), allTypes, true)
                .RegisterOf(typeof(TypeRefineries.ITypeRefiner), allTypes, true)
                .RegisterOf(typeof(MessageCreationStrategies.MessageCreationStrategy), allTypes, false)
                .RegisterOf(typeof(Composers.ComponentComposer), allTypes, false)
                .RegisterOf(typeof(ProtoWriters.ProtoWriter), allTypes, false)
                .AddSingleton<Utilities.IAssemblyLoader, Utilities.AssemblyLoader>()
                .AddSingleton<Utilities.ILogger, Utilities.ConsoleLogger>()
                
                .AddSingleton<Factories.IProtobufComponentFactory, Factories.ProtobufComponentFactory>()
                .AddSingleton<Factories.INameFactory, Factories.NameFactory>()
                .AddSingleton<Factories.IXTypeFactory, Factories.XTypeFactory>()
                .AddSingleton<Factories.IProtobufMessageComponentFactory, Factories.ProtobufMessageComponentFactory>()
                .AddSingleton<Factories.IProtoFileFactory, Factories.ProtoFileFactory>()
                
                .AddSingleton<Providers.IProtobufDataTypeProvider, Providers.ProtobufDataTypeProvider>()
                .AddSingleton<Providers.IProcedureParametersProvider, Providers.ProcedureParametersProvider>()
                
                .AddSingleton<ElementNameStrategies.ServiceNameStrategy>()
                .AddSingleton<TypeRefineries.ITypeRefinery, TypeRefineries.TypeRefinery>()
                .AddSingleton<IEngine, Engine>()
                .Configure(configure);
        }

        private static IServiceCollection RegisterOf(this IServiceCollection services, Type baseType, IEnumerable<Type> types, bool baseTypeIsService)
        {
            var serviceTypes = types.Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(baseType)).ToArray();

            if (baseTypeIsService)
                for (var i = 0; i < serviceTypes.Length; i++)
                    services.AddSingleton(serviceType: baseType, implementationType: serviceTypes[i]);
            else
                for (var i = 0; i < serviceTypes.Length; i++)
                    services.AddSingleton(serviceTypes[i]);

            return services;
        }
    }
}