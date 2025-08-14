using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class GameOverIfLeaderDiedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _deadLeaders
            = GroupBuilder<GameScope>
                .With<Leader>()
                .And<PlayerCard>()
                .And<Dead>()
                .Build();

        private static IGameStateMachine GameStateMachine => ServiceLocator.Resolve<IGameStateMachine>();

        public void Execute()
        {
            foreach (var _ in _deadLeaders)
                GameStateMachine.PendState<GameOverGameState>();
        }
    }
}