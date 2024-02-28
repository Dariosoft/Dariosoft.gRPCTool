using System.Text.RegularExpressions;

namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IXTypeFactory
    {
        Models.XType Create(Type type);
    }

    class XTypeFactory(TypeRefineries.ITypeRefinery refinery) : IXTypeFactory
    {
        public Models.XType Create(Type type)
        {
            type = refinery.Refine(type, out var summary);
            var isTask = summary.WasATaskType;
            var isNullable = summary.WasANullableType;
         
            bool isVoid = type == typeof(void)
                , isDictionary = IsDictionary(type)
                , isArray = !isVoid && type != typeof(string) && !isDictionary && typeof(System.Collections.IEnumerable).IsAssignableFrom(type)
                , isBuffer = false;
            
            Type? arrayElementType = isArray ? GetArrayElementType(type) : null;

            if (isArray && arrayElementType == typeof(byte))
            {
                isArray = false;
                arrayElementType = null;
                isBuffer = true;
            }

            if (isArray)
                type = arrayElementType!;


            bool isPrimitive = !isVoid && type.IsPrimitive || type == typeof(string)
                , isEnum = !isPrimitive && type.IsEnum
                , isDateTimeRelated = type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(DateOnly) || type == typeof(TimeOnly)
                , isTimeSpan = type == typeof(TimeSpan)
                , isComplexStruct = !isDateTimeRelated && !isTimeSpan && IsComplexStruct(type)
                , isTuple = Regex.IsMatch(input: type.FullName ?? type.Name, pattern: "^System.(Value)?Tuple`\\d+");

            var result = new Models.XType(
                IsArray: isArray,
                IsBuffer: isBuffer || type.IsAssignableTo(typeof(Stream)),
                IsComplexStruct: isComplexStruct,
                IsComplex: !isComplexStruct && !isVoid && !isPrimitive && !isArray && !isEnum && !isDictionary && !isTuple && (type.IsClass || type.IsInterface),
                IsDateTimeRelated: isDateTimeRelated,
                IsDictionary: isDictionary,
                IsEnum: isEnum,
                IsGeneric: type.IsGenericType,
                IsGuid: type == typeof(Guid),
                IsNullable: isNullable,
                IsPrimitive: isPrimitive,
                IsStream: type.IsAssignableTo(typeof(Stream)),
                IsTask: isTask,
                IsTuple: isTuple,
                IsTimeSpan: isTimeSpan,
                IsVoid: isVoid,
                // ArrayElementType: arrayElementType,
                DictionaryKeyType: isDictionary ? type.GenericTypeArguments[0] : null,
                DictionaryValueType: isDictionary ? type.GenericTypeArguments[1] : null)
            {
                Type = type,
                WellFormedName = type.GetWellFormedName()
            };

            return result;
        }
        
        private Type? GetArrayElementType(Type type)
            => type.IsArray ? type.GetElementType() : type.GenericTypeArguments[0];

        private bool IsDictionary(Type type)
        {
            return type.GetInterfaces().Any(interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }
        
        private bool IsComplexStruct(Type type)
        {
            return type is { IsValueType: true, IsPrimitive: false, IsEnum: false } && 
                   type != typeof(Guid) &&
                   (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>));
        }
    }
}