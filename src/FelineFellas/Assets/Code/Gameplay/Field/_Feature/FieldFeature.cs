namespace FelineFellas
{
    public sealed class FieldFeature : Feature
    {
        public FieldFeature()
            : base(nameof(FieldFeature))
        {
            Add(new SpawnFieldSystem());
            Add(new SpawnRowsSystem());
            Add(new SpawnCellsSystem());

            Add(new PutLeadsOnFieldSystem());
        }
    }
}