namespace FelineFellas
{
    public sealed class DragAndDropFeature : Feature
    {
        public DragAndDropFeature()
            : base(nameof(DragAndDropFeature))
        {
            Add(new StartDraggingSystem());
            Add(new DragEntitySystem());
            Add(new DropEntitiesSystem());

            Add(new CleanupDroppedSystem());
        }
    }
}