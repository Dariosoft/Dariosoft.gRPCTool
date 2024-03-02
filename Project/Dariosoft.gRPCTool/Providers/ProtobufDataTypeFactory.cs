using Dariosoft.gRPCTool.Factories;
using Dariosoft.gRPCTool.Utilities;

namespace Dariosoft.gRPCTool.Providers
{
    public interface IProtobufDataTypeProvider
    {
        Models.ProtobufDateTypeInfo Provide(Models.XType type, EnqueueElement enqueue);
    }

    class ProtobufDataTypeProvider(
        INameFactory nameFactory,
        IXTypeFactory xTypeFactory
        ) : IProtobufDataTypeProvider
    {
        public Models.ProtobufDateTypeInfo Provide(Models.XType xType, EnqueueElement enqueue)
        {
            Models.ProtobufDateTypeInfo? info = null;

            if (xType.IsNullable && Utilities.GoogleProtobuf.NullableTypeMap.TryGetValue(xType.Type, out var typeName))
            {
                if (xType.IsArray) typeName = $"repeated {typeName}";
                info = new Models.ProtobufDateTypeInfo(typeName, false, false);
            }

            if (info is null && Utilities.GoogleProtobuf.TypeMap.TryGetValue(xType.Type, out typeName))
            {
                if (xType.IsArray) typeName = $"repeated {typeName}";
                info = new Models.ProtobufDateTypeInfo(typeName, xType is { IsNullable: true, IsArray: false }, false);
            }
  
            if (info is null && (xType.IsStream || xType.IsBuffer))
            {
                info = new Models.ProtobufDateTypeInfo(Utilities.GoogleProtobuf.Bytes, false, false);
            }

            if (info is null && xType.IsArray)
            {
                var elementType = Provide(xTypeFactory.Create(xType.Type), enqueue);
                typeName = $"repeated {elementType.TypeName}";
                info = new Models.ProtobufDateTypeInfo(typeName, false, false);
            }

            if (info is null && xType.IsDictionary)
            {
                var keyType = Provide(xTypeFactory.Create(xType.DictionaryKeyType!), enqueue);
                var valType = Provide(xTypeFactory.Create(xType.DictionaryValueType!), enqueue);
                info = new Models.ProtobufDateTypeInfo($"map<{keyType.TypeName},{valType.TypeName}>", false, false);
            }
            
            if (info is null &&  xType.IsEnum)
            {
                var enumElement = new Elements.EnumElement(xType.Type);
                enqueue(enumElement);
                typeName = nameFactory.Create(enumElement).ProtobufName; 
                info = new Models.ProtobufDateTypeInfo(typeName, xType.IsNullable, false);
            }
            
            if (info is null && (xType.IsComplex || xType.IsComplexStruct))
            {
                var messageElement = Elements.MessageElement.DataMessage(xType);
                enqueue(messageElement);
                typeName = nameFactory.Create(messageElement).ProtobufName;
                info = new Models.ProtobufDateTypeInfo(typeName, false, false);
            }
            
            info ??= FromCustomProviders(xType);
            info ??= new Models.ProtobufDateTypeInfo(Utilities.Constants.UnknownMessage, false, false);

            return info;
        }

        private Models.ProtobufDateTypeInfo? FromCustomProviders(Models.XType xType)
        {
            //TODO: Implement this
            return null;
        }
    }
}