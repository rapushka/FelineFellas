using Entitas.Generic;

namespace FelineFellas
{
    public interface IStageFactory : IService
    {
        Entity<GameScope> CreateMockForPlayer();
        Entity<GameScope> Create(int number, EntityID mapID);
    }

    public class StageFactory : IStageFactory
    {
        public Entity<GameScope> CreateMockForPlayer()
            => CreateInternal(-1)
                .Add<PlayerStage>();

        public Entity<GameScope> Create(int number, EntityID mapID)
            => CreateInternal(number)
                .SetParent(mapID);

        private static Entity<GameScope> CreateInternal(int number)
            => CreateEntity.Empty()
                .Add<Name, string>("stage")
                .Add<Stage, StageID>(new(number))
                .Add<Initializing>();
    }
}