namespace Dariosoft.gRPCTool.V2.Utilities
{
    public sealed class DariosoftProtobuf
    {
        public const string ValueMessage_Byte = "ValueMessage_Byte";
        public const string ValueMessage_SByte = "ValueMessage_SByte";
        public const string ValueMessage_Decimal = "ValueMessage_Decimal";
        public const string ValueMessage_Guid = "ValueMessage_Guid";
        public const string ValueMessage_Short = "ValueMessage_Short";
        public const string ValueMessage_UShort = "ValueMessage_UShort";
        public const string ValueMessage_TimeOnly = "ValueMessage_TimeOnly";

        public static readonly IDictionary<Type, string> ValueMessages = new Dictionary<Type, string>
        {
            { typeof(CustomTypes.ByteValue), ValueMessage_Byte },
            { typeof(CustomTypes.SByteValue), ValueMessage_SByte },
            { typeof(CustomTypes.DecimalValue), ValueMessage_Decimal },
            { typeof(CustomTypes.ShortValue), ValueMessage_Short },
            { typeof(CustomTypes.UShortValue), ValueMessage_UShort },
            { typeof(CustomTypes.GuidValue), ValueMessage_Guid },
            { typeof(CustomTypes.TimeOnlyValue), ValueMessage_TimeOnly },
        };
    }
}