using Entitas.Generic;

namespace FelineFellas
{
    public interface IEcsRunner : IService
    {
        void Initialize();

        void StartGame();

        void OnUpdate();

        void EndGame();
    }

    public class EcsRunner : IEcsRunner
    {
        private GameplayFeature _feature;

        public void Initialize()
        {
            Contexts.Instance.InitializeScope<GameScope>();
            Contexts.Instance.InitializeScope<InputScope>();

            Contexts.Instance.Get<GameScope>().GetPrimaryIndex<ID, EntityID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetIndex<CardInDeck, EntityID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetIndex<ChildOf, EntityID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetIndex<AbilityTemplate, EntityID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetIndex<AbilityUse, EntityID>().Initialize();

            Contexts.Instance.Get<GameScope>().GetPrimaryIndex<Stage, StageID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetPrimaryIndex<ActorOnStage, StageID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetPrimaryIndex<LeadOnStage, StageID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetPrimaryIndex<DeckOnStage, StageID>().Initialize();
            Contexts.Instance.Get<GameScope>().GetIndex<CardOnStage, StageID>().Initialize();

#if DEBUG
            Entity<GameScope>.Formatter = new GameEntityFormatter();
#endif

            CustomIndexes.Initialize();
        }

        public void StartGame()
        {
            _feature = new();
            _feature.ActivateReactiveSystems();
            _feature.Initialize();
        }

        public void OnUpdate()
        {
            if (_feature is null)
                return;

            _feature.Execute();
            _feature.Cleanup();
        }

        public void EndGame()
        {
            if (_feature is null)
                return;

            _feature.DeactivateReactiveSystems();
            _feature.ClearReactiveSystems();

            DisposeEntities();

            _feature.Cleanup();
            _feature.TearDown();

            _feature.Remove(_feature);

#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
            _feature.gameObject.DestroyObject();
#endif

            _feature = null;
        }

        private void DisposeEntities()
        {
            foreach (var entity in Contexts.Instance.Get<GameScope>().GetEntities())
                entity.Add<Destroy>();

            foreach (var entity in Contexts.Instance.Get<InputScope>().GetEntities())
                entity.Destroy();
        }
    }
}