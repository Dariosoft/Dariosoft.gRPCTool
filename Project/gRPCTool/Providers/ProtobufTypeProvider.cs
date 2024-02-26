using Dariosoft.gRPCTool.Factories;

namespace Dariosoft.gRPCTool.Providers
{
    public interface IProtobufTypeInfoProvider
    {
        Models.ProtobufTypeInfo? Provide(Models.TypeDescriptor descriptor, Action<Type> pushIntoMessageFactoryQueue);

        bool Enabled { get; }

        string Priority { get; }
    }

    public interface IProtobufTypeProvider
    {
        Models.ProtobufTypeInfo Provide(Type type);
        
        Models.ProtobufTypeInfo Provide(Models.TypeDescriptor typeDescriptor);
    }

    internal class ProtobufTypeProvider(
        Analyzers.ITypeAnalyzer typeAnalyzer,
        INameFactory nameFactory,
        Accessories.IMessageFactoryQueueFeeder messageFeeder,
        Accessories.IEnumFactoryQueueFeeder enumQFeeder,
        IEnumerable<IProtobufTypeInfoProvider> typeInfoProviders
    ) : IProtobufTypeProvider
    {
        private readonly IProtobufTypeInfoProvider[] _typeInfoProviders = typeInfoProviders.Where(p => p.Enabled).OrderBy(p => p.Priority).ToArray();

        public Models.ProtobufTypeInfo Provide(Type type) => Provide(typeAnalyzer.Analyze(type));
        public Models.ProtobufTypeInfo Provide(Models.TypeDescriptor typeDescriptor)
        {
            Models.ProtobufTypeInfo? info = null;

            if (typeDescriptor.IsNullable && Protobuf.GoogleProtobuf.NullableTypeMap.TryGetValue(typeDescriptor.Type, out var typeName))
            {
                if (typeDescriptor.IsArray) typeName = $"repeated {typeName}";
                info = new Models.ProtobufTypeInfo(typeName, false);
            }

            if (info is null && Protobuf.GoogleProtobuf.TypeMap.TryGetValue(typeDescriptor.Type, out typeName))
            {
                if (typeDescriptor.IsArray) typeName = $"repeated {typeName}";
                info = new Models.ProtobufTypeInfo(typeName, typeDescriptor is { IsNullable: true, IsArray: false });
            }

            if (info is null && (typeDescriptor.IsStream || typeDescriptor.IsBuffer))
            {
                info = new Models.ProtobufTypeInfo(Protobuf.GoogleProtobuf.Bytes, false);
            }

            if (info is null && typeDescriptor.IsArray)
            {
                var elementType = Provide(typeAnalyzer.Analyze(typeDescriptor.Type));
                typeName = $"repeated {elementType.TypeName}";
                info = new Models.ProtobufTypeInfo(typeName, false);
            }

            if (info is null && typeDescriptor.IsDictionary)
            {
                var keyType = Provide(typeAnalyzer.Analyze(typeDescriptor.DictionaryKeyType!));
                var valType = Provide(typeAnalyzer.Analyze(typeDescriptor.DictionaryValueType!));
                info = new Models.ProtobufTypeInfo($"map<{keyType.TypeName},{valType.TypeName}>", false);
            }

            if (info is null &&  typeDescriptor.IsEnum)
            {
                var enumElement = new ElementTypes.EnumElement(typeDescriptor.Type);
                enumQFeeder.Enqueue(enumElement);
                //var enumName = nameFactory.Create();
                typeName = nameFactory.Create(enumElement).ProtobufName; 
                info = new Models.ProtobufTypeInfo(typeName, typeDescriptor.IsNullable);
            }

            if (info is null && (typeDescriptor.IsComplex || typeDescriptor.IsComplexStruct))
            {
                var messageElement = new ElementTypes.MessageElement(typeDescriptor);
                messageFeeder.Enqueue(messageElement);
                typeName = nameFactory.Create(messageElement).ProtobufName;
                info = new Models.ProtobufTypeInfo(typeName, false);
            }

            info ??= FromCustomProviders(typeDescriptor);
            info ??= new Models.ProtobufTypeInfo(Constants.UnknownMessage, false); 
            
            /* if (!resolved && typeDescriptor.IsGuid)
             {
                 typeName = Protobuf.CustomProtobuf.GuidValueMessage;
                 messageFeeder.Enqueue(typeDescriptor.Type);
                 if (typeDescriptor.IsArray)
                     typeName = $"repeated {typeName}";
                 resolved = true;
             }*/

            return info;
        }

        private Models.ProtobufTypeInfo? FromCustomProviders(Models.TypeDescriptor descriptor)
        {
            Models.ProtobufTypeInfo? info = null;

            for (var i = 0; i < _typeInfoProviders.Length && info is null; i++)
                info = _typeInfoProviders[i].Provide(descriptor, type => messageFeeder.Enqueue(new ElementTypes.MessageElement(typeAnalyzer.Analyze(type))));

            return info;
        }
    }
}