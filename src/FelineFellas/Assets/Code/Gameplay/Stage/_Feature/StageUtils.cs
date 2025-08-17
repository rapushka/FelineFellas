using Entitas.Generic;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public static class StageUtils
    {
        private static PrimaryEntityIndex<GameScope, Stage, StageID> StageIndex
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<Stage, StageID>();

        private static PrimaryEntityIndex<GameScope, DeckOnStage, StageID> DeckIndex
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<DeckOnStage, StageID>();

        private static PrimaryEntityIndex<GameScope, ActorOnStage, StageID> ActorIndex
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<ActorOnStage, StageID>();

        public static GameEntity GetStage(GameEntity entityOnStage)
            => GetStage(GetStageID(entityOnStage));

        public static GameEntity GetStage(StageID stageID)
            => StageIndex.GetEntity(stageID);

        public static GameEntity GetDeckForActor(GameEntity actor)
        {
            actor.AssertIs<Actor>();
            return GetDeckOnStage(actor.Get<ActorOnStage>().Value);
        }

        public static GameEntity GetActorForLead(GameEntity lead)
        {
            lead.AssertIs<Leader>();
            return GetActorOnStage(lead.Get<LeadOnStage>().Value);
        }

        public static GameEntity GetDeckOnStage(StageID stageID)  => DeckIndex.GetEntity(stageID);
        public static GameEntity GetActorOnStage(StageID stageID) => ActorIndex.GetEntity(stageID);

        public static StageID GetStageID(GameEntity entity)
            => entity.TryGet<Stage, StageID>(out var stageID)       ? stageID
                : entity.TryGet<ActorOnStage, StageID>(out stageID) ? stageID
                : entity.TryGet<DeckOnStage, StageID>(out stageID)  ? stageID
                : entity.TryGet<LeadOnStage, StageID>(out stageID)  ? stageID
                : entity.TryGet<CardOnStage, StageID>(out stageID)  ? stageID
                                                                      : throw new($"{entity} is not on stage!");
    }

    public static class StageUtilsExtensions
    {
        public static GameEntity GetOwnedDeck(this GameEntity actor) => StageUtils.GetDeckForActor(actor);

        public static GameEntity CopyStage<TComponent>(this GameEntity @this, GameEntity from)
            where TComponent : ValueComponent<StageID>, IInScope<GameScope>, new()
        {
            var stageID = StageUtils.GetStageID(from);
            @this.Set<TComponent, StageID>(stageID);

            return @this;
        }
    }
}