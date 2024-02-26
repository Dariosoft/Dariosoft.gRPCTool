using Dariosoft.gRPCTool.Models;

namespace Dariosoft.gRPCTool.Protobuf
{
    /*public sealed class CustomProtobuf
    {
        private const string ValueMessage = "GrpcValueMessage__";

        public const string UnknownMessage = "GrpcUnknownMessage";
        public const string BytesMessage = "GrpcBytesMessage";
        public const string DateTimeValueMessage = ValueMessage + "DateTime";
        public const string TimeSpanValueMessage = ValueMessage + "TimeSpan";
        public const string GuidValueMessage = ValueMessage + "Guid";

        public static string GetPrimitiveValueMessage(TypeDescriptor type)
        {
            if (type.IsGuid)
                return GuidValueMessage;
            else if (type.IsDateTimeRelated)
                return DateTimeValueMessage;
            else if (type.IsTimeSpan)
                return TimeSpanValueMessage;
            else
                return ValueMessage + type.WellFormedName.AsNameIdentifier(true);
        }

        public static string GetArrayMessage(TypeDescriptor type)
        {
            var name = type.WellFormedName.AsNameIdentifier(true);
            return $"GrpcArrayMessage__{name}";
        }

        public static string GetDictionaryMessage(TypeDescriptor type)
        {
            var name = type.WellFormedName.AsNameIdentifier(true);
            return $"GrpcDictionaryMessage_{name}";
        }

        public static string GetTupleMessage(TypeDescriptor type)
        {
            var name = type.WellFormedName.AsNameIdentifier(true);
            return $"GrpcTupleMessage_{name}";
        }

        public static string GetEnumMessage(Type enumType)
        {
            var name = enumType.GetWellFormedName().AsNameIdentifier(true);
            return $"GrpcEnum_{name}";
        }


        public static string GetComplexTypeMessage(TypeDescriptor type)
        {
            var name = type.WellFormedName.AsNameIdentifier(true);
            return $"Grpc{name}";
        }
    }*/
}
