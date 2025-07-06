using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class UnitUtils
    {
        private static readonly IGroup<Entity<GameScope>> Leaders
            = GroupBuilder<GameScope>
                .With<UnitCard>()
                .And<Leader>()
                .Build();

        public static Entity<GameScope> GetLeader(Side side)
            => Leaders.First(leader => leader.Get<OnSide>().Value == side);
    }
}