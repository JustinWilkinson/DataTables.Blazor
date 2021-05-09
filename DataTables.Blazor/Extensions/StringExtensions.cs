namespace DataTables.Blazor.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str) => $"{char.ToLower(str[0])}{str[1..]}";
    }
}
