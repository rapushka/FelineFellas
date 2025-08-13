using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class PutLeadsOnFieldSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<ActiveActor>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _leaders
            = GroupBuilder<GameScope>
                .With<Leader>()
                .And<LayingOnDeck>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static int RowCenter => GameConfig.Field.RowWidth / 2;

        private static ScopeContext<GameScope> Context => Contexts.Instance.Get<GameScope>();

        public void Execute()
        {
            foreach (var _ in _stages)
            foreach (var actor in _actors)
            foreach (var leader in _leaders)
            {
                if (!actor.OnSameSide(leader))
                    continue;

                var side = actor.Get<OnSide>().Value;
                var cell = Context.GetCell(side, RowCenter);

                CardUtils.PlaceCardOnField(leader, cell);
            }
        }
    }
}