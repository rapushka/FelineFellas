namespace FelineFellas.Editor
{
    internal static class StringSerializationExtensions
    {
        internal static string AsSerializedProperty(this string source)
            => $"<{source}>k__BackingField";
    }
}