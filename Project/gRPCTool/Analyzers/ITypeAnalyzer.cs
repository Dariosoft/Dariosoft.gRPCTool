using System.Text.RegularExpressions;

namespace Dariosoft.gRPCTool.Analyzers
{
    public interface ITypeAnalyzer : IAnalyzer
    {
        Models.TypeDescriptor Analyze(Type type);

        bool IsVoid(Type type);
    }

    internal class TypeAnalyzer : ITypeAnalyzer
    {
        public bool IsVoid(Type type)
        {
            return type == typeof(void) ||
                type == typeof(Task) ||
                type == typeof(ValueTask);
        }

        public Models.TypeDescriptor Analyze(Type type)
        {
            type = ResolveTaskType(type, out var isTask);
            type = ResolveNullableType(type, out var isNullable);

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


            var result = new Models.TypeDescriptor(
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
        
        private Type ResolveTaskType(Type type, out bool isTask)
        {
            isTask = false;

            if (type.Name is "Task`1" or "ValueTask`1")
            {
                type = type.GenericTypeArguments[0];
                isTask = true;
            }
            else if (type.Name is "Task" or "ValueTask")
            {
                type = typeof(void);
                isTask = true;
            }

            return type;
        }

        private Type ResolveNullableType(Type type, out bool isNullable)
        {
            isNullable = type.Name is "Nullable`1";
            return isNullable ? type.GenericTypeArguments[0] : type;
        }

        private bool IsDictionary(Type type)
        {
            return type.GetInterfaces().Any(interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IDictionary<,>));
        }

        private Type? GetArrayElementType(Type type)
            => type.IsArray ? type.GetElementType() : type.GenericTypeArguments[0];

        private bool IsComplexStruct(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && !type.IsEnum && type != typeof(Guid) &&
                   (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>));
        }
    }
}
