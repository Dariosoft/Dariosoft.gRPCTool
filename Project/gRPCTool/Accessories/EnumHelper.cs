namespace Dariosoft.gRPCTool.Accessories
{
    internal class EnumHelper
    {
        private EnumHelper() { }

        private static readonly Lazy<EnumHelper> _lazy = new Lazy<EnumHelper>(() => new EnumHelper());

        public static EnumHelper Instance => _lazy.Value;

        #region GetItems
        public IDictionary<int, string> GetItems(Type enumType)
        {
            if (!enumType.IsEnum)
                return new Dictionary<int, string>();

            return Enum.GetNames(enumType)
                .Select(name => new { Name = name, Val = Convert.ToInt32(Enum.Parse(enumType, name)) })
                .OrderBy(e => e.Val)
                .ToDictionary(e => e.Val, e => e.Name);
        }

        public IDictionary<int, string> GetItems<TEnum>()
            where TEnum : Enum
        {
            return GetItems(typeof(TEnum));
        }
        #endregion
    }
}
