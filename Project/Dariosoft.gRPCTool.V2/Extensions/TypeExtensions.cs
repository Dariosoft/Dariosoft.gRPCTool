namespace Dariosoft.gRPCTool.V2
{
    public static class TypeExtensions
    {
        public static string GetWellFormedName(this Type? input)
        {
            if (input is null) return "";

            var name = input.FullName ?? input.Name;

            var i = name.IndexOf('`');
            if (i > 0) name = name[..i];

            if (input.GenericTypeArguments.Length > 0)
            {
                var generics = new string[input.GenericTypeArguments.Length];

                for (i = 0; i < input.GenericTypeArguments.Length; i++)
                {
                    generics[i] = GetWellFormedName(input.GenericTypeArguments[i]);
                }

                name = $"{name}<{string.Join(", ", generics)}>";
            }

            return name;
        }

        public static string GetNameIdentifier(Type type)
            => GetWellFormedName(type).AsNameIdentifier();

        public static bool IsTakType(this Type type)
        {
            return type.Name is "Task`1" or "ValueTask`1" or "Task" or "ValueTask";
        }
        
        public static bool IsNullableType(this Type type)
        {
            return type.Name is "Nullable`1";
        }
    }
}
