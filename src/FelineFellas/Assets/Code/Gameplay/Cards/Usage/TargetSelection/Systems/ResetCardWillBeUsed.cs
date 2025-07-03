using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ResetCardWillBeUsed : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                // TODO: needed?
                // .And<Dragging>()
                .Build();

        public void Execute()
        {
            foreach (var card in _draggedCard)
            {
                card
                    .Is<WillBeUsed>(false)
                    .RemoveSafely<DropCardOn>()
                    ;
            }
        }
    }
}