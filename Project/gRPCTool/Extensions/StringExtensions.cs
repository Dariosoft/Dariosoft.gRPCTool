using System.Text;
using System.Text.RegularExpressions;

namespace Dariosoft.gRPCTool
{

    public static class StringExtensions
    {
        public static string ToPublicCase(this string input)
         => string.IsNullOrWhiteSpace(input) ? input : $"{char.ToUpper(input[0])}{input[1..]}";

        public static string ToPrivateCase(this string input)
            => string.IsNullOrWhiteSpace(input) ? input : $"{char.ToLower(input[0])}{input[1..]}";

        public static string RemoveSystemNamespace(this string name)
            => Regex.Replace(input: name, pattern: @"\bSystem\.", replacement: "");

        public static string AsNameIdentifier(this string input, bool removeSystemNamespace = true)
        {
            if (removeSystemNamespace)
                input = RemoveSystemNamespace(input);

            return input.Replace("[", "_").Replace("]", "_")
               .Replace("<", "_").Replace(">", "_")
               .Replace(",", "_").Replace(".", "_").Replace(" ", "_");
        }

        public static string ToHexString(this string input)
        {
            if (input == null)
                return string.Empty;
            else if (input == "")
                return "0x";
            else
                return $"0x{BitConverter.ToString(Encoding.UTF8.GetBytes(input)).Replace("-", "")}";
        }

        public static string ComputeHash(this string plainText)
        {
            var result = string.Empty;
            using (var sha512 = System.Security.Cryptography.SHA512.Create())
            {
                result = BitConverter.ToString(sha512.ComputeHash(Encoding.UTF8.GetBytes(plainText))).Replace("-", "");
                sha512.Clear();
            }

            return result;
        }
    }
}
