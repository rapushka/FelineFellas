namespace FelineFellas
{
    public sealed class InputFeature : Feature
    {
        public InputFeature()
            : base(nameof(InputFeature))
        {
            Add(new InitializeInputSystem());
            Add(new UpdateMousePositionSystem());
        }
    }
}