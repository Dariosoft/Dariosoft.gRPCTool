namespace Dariosoft.gRPCTool.Utilities
{
    public class EnumHelper
    {
        private EnumHelper() { }

        private static readonly Lazy<EnumHelper> _lazy = new Lazy<EnumHelper>(() => new EnumHelper());

        public static EnumHelper Instance => _lazy.Value;

        #region GetItems
        public IDictionary<int, string> GetItems(Type enumType, string namePrefix = "")
        {
            if (!enumType.IsEnum)
                return new Dictionary<int, string>();

            return Enum.GetNames(enumType)
                .Select(name => new { Key = Convert.ToInt32(Enum.Parse(enumType, name)), Value = namePrefix + name })
                .DistinctBy(x => x.Key)
                .OrderBy(e => e.Key)
                .ToDictionary(e => e.Key, e => e.Value);
        }

        public IDictionary<int, string> GetItems<TEnum>(string namePrefix = "")
            where TEnum : Enum
        {
            return GetItems(typeof(TEnum),namePrefix);
        }
        #endregion
    }
}