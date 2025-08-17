using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public interface IActorFactory : IService
    {
        GameEntity CreatePlayer(LoadoutConfig loadout, StageID mockStageID);
        GameEntity CreateEnemyOnMap(LoadoutConfig loadout, EntityID stageID);
    }

    public class ActorFactory : IActorFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        private static IDeckFactory DeckFactory => ServiceLocator.Resolve<IDeckFactory>();

        public GameEntity CreatePlayer(LoadoutConfig loadout, StageID mockStageID)
        {
            var actor = Create(loadout, Side.Player)
                    .Add<Name, string>("player")
                    .Add<PlayerActor>()
                    .Add<Money, int>(GameConfig.Money.MoneyOnStart)
                    .Add<ActorOnStage, StageID>(mockStageID)
                    .Chain(a => DeckFactory.Create(a, loadout))
                    .Chain(a => CreateLeadOnDeck(a, loadout))
                    .Add<ActiveActor>()
                ;

            return actor;
        }

        public GameEntity CreateEnemyOnMap(LoadoutConfig loadout, EntityID stageEntityID)
        {
            var stage = stageEntityID.GetEntity();
            var stageID = stage.Get<Stage, StageID>();

            var actor = Create(loadout, Side.Enemy)
                    .Add<Name, string>("enemy")
                    .Add<EnemyActor>()
                    .Add<ActorOnStage, StageID>(stageID)
                    .Chain(a => CreateLeadOnMap(a, loadout))
                    .Add<EnemyLoadout, LoadoutConfig>(loadout)
                ;

            return actor;
        }

        private GameEntity Create(LoadoutConfig loadout, Side side)
        {
            var actor = CreateEntity.Empty()
                    .Add<Actor>()
                    .Add<HandSize, int>(loadout.HandSize)
                    .Add<OnSide, Side>(side)
                ;

            return actor;
        }

        private GameEntity CreateLeadOnDeck(GameEntity actor, LoadoutConfig loadout)
        {
            var deck = actor.GetOwnedDeck();

            CardFactory.CreateLeadOnDeck(loadout.Lead, deck);

            return actor;
        }

        private GameEntity CreateLeadOnMap(GameEntity actor, LoadoutConfig loadout)
        {
            var stage = actor.Get<ActorOnStage>().Value;
            CardFactory.CreateEnemyLeadOnMap(loadout.Lead, stage)
                .SetParent(actor)
                ;

            return actor;
        }
    }
}