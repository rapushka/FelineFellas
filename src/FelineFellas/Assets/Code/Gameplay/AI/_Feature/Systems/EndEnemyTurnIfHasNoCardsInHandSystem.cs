using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class EndEnemyTurnIfHasNoCardsInHandSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediator
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<InEnemyTurnState>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<ActiveEnemyActor>()
                .Build();

        public void Execute()
        {
            foreach (var mediator in _turnMediator)
            foreach (var enemy in _enemies)
            {
                var cardsInHand = ActorUtils.GetCardsInHand(enemy);
                var playableCards = cardsInHand.Count(c => !c.Is<CanNotPlay>());

                if (playableCards == 0)
                    mediator.Is<ToNextTurnState>(true);
            }
        }
    }
}