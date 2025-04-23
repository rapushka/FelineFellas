namespace FelineFellas
{
    public sealed class InputFeature : Feature
    {
        public InputFeature()
            : base(nameof(InputFeature))
        {
            Add(new InitializeInputSystem());
            Add(new UpdateMousePositionSystem());

            Add(new ResetHoveredSystem());
            Add(new UpdateHoverSystem());
        }
    }
}