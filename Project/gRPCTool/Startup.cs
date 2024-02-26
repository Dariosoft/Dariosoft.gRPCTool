using Microsoft.Extensions.DependencyInjection;

namespace Dariosoft.gRPCTool
{
    public static class Startup
    {
        public static IServiceCollection AddgRPCTool(this IServiceCollection services, Action<Options> configure)
        {
            var allTypes = typeof(Startup).Assembly.GetTypes();

            return services
                .RegisterMessageBuildStrategies(allTypes)
                .RegisterNameBuildStrategies(allTypes)
                .AddSingleton<Accessories.ILogger, Accessories.ConsoleLogger>()
                .AddSingleton<Accessories.IAssemblyLoader, Accessories.AssemblyLoader>()
                .AddSingleton<Accessories.IElementPool, Accessories.ElementPool>()
                .AddSingleton<Accessories.MessageFactoryQueue>()
                .AddSingleton<Accessories.IMessageFactoryQueueFeeder>(sp => sp.GetService<Accessories.MessageFactoryQueue>()!)
                .AddSingleton<Accessories.IMessageFactoryQueueReader>(sp => sp.GetService<Accessories.MessageFactoryQueue>()!)
                .AddSingleton<Accessories.EnumFactoryQueue>()
                .AddSingleton<Accessories.IEnumFactoryQueueFeeder>(sp => sp.GetService<Accessories.EnumFactoryQueue>()!)
                .AddSingleton<Accessories.IEnumFactoryQueueReader>(sp => sp.GetService<Accessories.EnumFactoryQueue>()!)
                .AddSingleton<IEngine, Engine>()
                .AddSingleton<Factories.IProtobufHeaderFactory, Factories.ProtobufHeaderFactory>()
                .AddSingleton<Factories.IServiceFactory, Factories.ServiceFactory>()
                .AddSingleton<Factories.IProcedureFactory, Factories.ProcedureFactory>()
                //.AddSingleton<Factories.IRpcRequestMessageNameFactory, Factories.RpcRequestMessageNameFactory>()
                .AddSingleton<Factories.IRpcRequestMessageFactory, Factories.RpcRequestMessageFactory>()
                .AddSingleton<Factories.IMessageFactory, Factories.MessageFactory>()
                .AddSingleton<Factories.IMessageMemberFactory, Factories.MessageMemberFactory>()
                .AddSingleton<Factories.IProtobufFactory, Factories.ProtobufFactory>()
               // .AddSingleton<Factories.IProcedureNameFactory, Factories.ProcedureNameFactory>()
                .AddSingleton<Factories.IEnumMessageFactory, Factories.EnumMessageFactory>()
                //.AddSingleton<Factories.IServiceNameFactory, Factories.ServiceNameFactory>()
                .AddSingleton<ProtobufWriters.IProtobufWriter, ProtobufWriters.ProtobufWriter>()
                .AddSingleton<ProtobufWriters.IProtobufHeaderWriter, ProtobufWriters.ProtobufHeaderWriter>()
                .AddSingleton<ProtobufWriters.IProtobufServiceWriter, ProtobufWriters.ProtobufServiceWriter>()
                .AddSingleton<ProtobufWriters.IProtobufMessageWriter, ProtobufWriters.ProtobufMessageWriter>()
                .AddSingleton<ProtobufWriters.IProtobufEnumMessageWriter, ProtobufWriters.ProtobufEnumMessageWriter>()
                .AddSingleton<Providers.IServiceTypeProvider, Providers.ServiceTypeProvider>()
                .AddSingleton<Providers.IProcedureMethodProvider, Providers.ProcedureProvider>()
                .AddSingleton<Providers.IProtobufTypeProvider, Providers.ProtobufTypeProvider>()
                .AddSingleton<Analyzers.ITypeAnalyzer, Analyzers.TypeAnalyzer>()
                /*
                .AddSingleton<Analyzers.IServiceAnalyzer, Analyzers.ServiceAnalyzer>()

                .AddSingleton<Providers.INameProvider, Providers.NameProvider>()
                .AddSingleton<Providers.IRpcRequestMessageNameProvider, Providers.RpcRequestMessageNameProvider>()
                .AddSingleton<Providers.IRpcResponseMessageNameProvider, Providers.RpcResponseMessageNameProvider>()
                .AddSingleton<Providers.IComplexMessageNameProvider, Providers.ComplexMessageNameProvider>()

                .AddSingleton<Protobuf.IProtobufGenerator, Protobuf.ProtobufGenerator>()
                .AddSingleton<Protobuf.IProtobufHeaderWriter, Protobuf.ProtobufHeaderWriter>()
                .AddSingleton<Protobuf.IProtobufServiceWriter, Protobuf.ProtobufServiceWriter>()

                .AddSingleton<Factories.IMessageDescriptorFactory, Factories.MessageDescriptorFactory> ()
                */
                .Configure(configure);
        }

        private static IServiceCollection RegisterMessageBuildStrategies(this IServiceCollection services, IEnumerable<Type> allTypes)
        {
            var interfaceType = typeof(Factories.MessageBuilders.IMessageBuildStrategy);

            var strategyConcreteTypes = allTypes.Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(interfaceType)).ToArray();

            for (var i = 0; i < strategyConcreteTypes.Length; i++)
                services.AddSingleton(serviceType: interfaceType, implementationType: strategyConcreteTypes[i]);
            
            return services; //.AddSingleton<Factories.MessageBuilders.MessageBuildStrategySelector>();
        }
        
        private static IServiceCollection RegisterNameBuildStrategies(this IServiceCollection services, IEnumerable<Type> allTypes)
        {
            var interfaceType = typeof(Factories.ElementNameBuilders.INameBuildStrategy);

            var strategyConcreteTypes = allTypes.Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(interfaceType)).ToArray();

            for (var i = 0; i < strategyConcreteTypes.Length; i++)
                services.AddSingleton(serviceType: interfaceType, implementationType: strategyConcreteTypes[i]);
            
            return services.AddSingleton<Factories.INameFactory, Factories.NameFactory>();
        }
    }
}