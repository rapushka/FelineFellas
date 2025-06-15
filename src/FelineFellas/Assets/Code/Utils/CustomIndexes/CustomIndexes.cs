using Entitas.Generic;

namespace FelineFellas
{
    public static class CustomIndexes
    {
        public static void Initialize()
        {
            new CellCoordinatesPrimaryIndex(Contexts.Instance.Get<GameScope>()).Initialize();
        }
    }
}