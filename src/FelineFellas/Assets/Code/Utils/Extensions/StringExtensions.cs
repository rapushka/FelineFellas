namespace FelineFellas
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string @this) => string.IsNullOrWhiteSpace(@this);
    }
}