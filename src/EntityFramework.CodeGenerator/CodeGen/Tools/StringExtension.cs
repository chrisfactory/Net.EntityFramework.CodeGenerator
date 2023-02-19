namespace EntityFramework.CodeGenerator
{
    internal static class StringExtension
    {
        public static string ToCamelCase(this string str)
        {
            return System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(str);
        }

    }
}
