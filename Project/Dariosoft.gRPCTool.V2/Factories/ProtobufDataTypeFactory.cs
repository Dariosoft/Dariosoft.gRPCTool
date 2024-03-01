namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IProtobufDataTypeFactory
    {
        Models.ProtobufDateTypeInfo Create(Models.XType type, Delegates.EnqueueElement enqueue);
    }

    class ProtobufDataTypeFactory(
        INameFactory nameFactory,
        IXTypeFactory xTypeFactory
        ) : IProtobufDataTypeFactory
    {
        public Models.ProtobufDateTypeInfo Create(Models.XType xType, Delegates.EnqueueElement enqueue)
        {
            Models.ProtobufDateTypeInfo? info = null;

            if (xType.IsNullable && Utilities.GoogleProtobuf.NullableTypeMap.TryGetValue(xType.Type, out var typeName))
            {
                if (xType.IsArray) typeName = $"repeated {typeName}";
                info = new Models.ProtobufDateTypeInfo(typeName, false);
            }

            if (info is null && Utilities.GoogleProtobuf.TypeMap.TryGetValue(xType.Type, out typeName))
            {
                if (xType.IsArray) typeName = $"repeated {typeName}";
                info = new Models.ProtobufDateTypeInfo(typeName, xType is { IsNullable: true, IsArray: false });
            }

            if (info is null && (xType.IsStream || xType.IsBuffer))
            {
                info = new Models.ProtobufDateTypeInfo(Utilities.GoogleProtobuf.Bytes, false);
            }

            if (info is null && xType.IsArray)
            {
                var elementType = Create(xTypeFactory.Create(xType.Type), enqueue);
                typeName = $"repeated {elementType.TypeName}";
                info = new Models.ProtobufDateTypeInfo(typeName, false);
            }

            if (info is null && xType.IsDictionary)
            {
                var keyType = Create(xTypeFactory.Create(xType.DictionaryKeyType!), enqueue);
                var valType = Create(xTypeFactory.Create(xType.DictionaryValueType!), enqueue);
                info = new Models.ProtobufDateTypeInfo($"map<{keyType.TypeName},{valType.TypeName}>", false);
            }
            
            if (info is null &&  xType.IsEnum)
            {
                var enumElement = new Elements.EnumElement(xType.Type);
                enqueue(enumElement);
                typeName = nameFactory.Create(enumElement).ProtobufName; 
                info = new Models.ProtobufDateTypeInfo(typeName, xType.IsNullable);
            }
            
            if (info is null && (xType.IsComplex || xType.IsComplexStruct))
            {
                var messageElement = Elements.MessageElement.DataMessage(xType);
                enqueue(messageElement);
                typeName = nameFactory.Create(messageElement).ProtobufName;
                info = new Models.ProtobufDateTypeInfo(typeName, false);
            }
            
            info ??= FromCustomProviders(xType);
            info ??= new Models.ProtobufDateTypeInfo(Utilities.Constants.UnknownMessage, false);

            return info;
        }

        private Models.ProtobufDateTypeInfo? FromCustomProviders(Models.XType xType)
        {
            //TODO: Implement this
            return null;
        }
    }
}