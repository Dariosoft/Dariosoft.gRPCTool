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
    }
}