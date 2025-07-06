using Entitas.Generic;

namespace FelineFellas
{
    public interface IAbilityFactory : IService
    {
        Entity<GameScope> Create(EntityID cardID, AbilityConfig config);
    }

    public class AbilityFactory : IAbilityFactory
    {
        public Entity<GameScope> Create(EntityID cardID, AbilityConfig config)
        {
            return CreateEntity.Empty()
                    .Add<Name, string>("ability")
                    .Add<AbilityTemplate, EntityID>(cardID)
                    .Chain(a => AbilityUtils.Assign(a, config))
                ;
        }
    }
}