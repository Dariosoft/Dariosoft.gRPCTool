namespace Dariosoft.gRPCTool.V2.Models
{
    public record XType(
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
        
        public override string ToString() => WellFormedName;

        public static XType Void()
            => new XType(
                IsArray: false,
                IsBuffer: false,
                IsComplexStruct: false,
                IsComplex: false,
                IsDateTimeRelated: false,
                IsDictionary: false,
                IsEnum: false,
                IsGeneric: false,
                IsGuid: false,
                IsNullable: false,
                IsPrimitive: false,
                IsStream: false,
                IsTask: false,
                IsTuple: false,
                IsTimeSpan: false,
                IsVoid: true,
                // ArrayElementType: arrayElementType,
                DictionaryKeyType: null,
                DictionaryValueType: null)
            {
                Type = typeof(void),
                WellFormedName = "void"
            };
    }
}