namespace Dariosoft.gRPCTool.Models
{
    public record TypeDescriptor(
        bool IsArray,
        bool IsBuffer,
        bool IsComplexStruct,
        bool IsComplex,
        bool IsDateTimeRelated,
        bool IsDictionary,
        bool IsEnum,
        bool IsGeneric,
        bool IsGuid,
        bool IsNullable,
        bool IsPrimitive,
        bool IsStream,
        bool IsTask,
        bool IsTimeSpan,
        bool IsTuple,
        bool IsVoid,
        Type? DictionaryKeyType,
        Type? DictionaryValueType)
    {
        public Type Type { get; init; } = null!;
        public string WellFormedName { get; init; } = "";
        public Enums.MessageType MessageType
        {
            get
            {
                if (IsArray) return Enums.MessageType.Array;
                if (IsBuffer || IsStream) return Enums.MessageType.Buffer;
                if (IsComplex) return Enums.MessageType.Complex;
                if (IsDictionary) return Enums.MessageType.Dictionary;
                if (IsEnum) return Enums.MessageType.Enum;
                if (IsGuid) return Enums.MessageType.Guid;
                
                return Enums.MessageType.Simple;
            }
        }

        public List<string> Labels { get; } = [];
        
        public override string ToString() => WellFormedName;
    }
}
