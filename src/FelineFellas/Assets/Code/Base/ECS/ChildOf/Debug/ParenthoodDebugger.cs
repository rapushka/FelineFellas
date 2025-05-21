using System.Collections.Generic;
using System.Linq;
using Entitas.Generic;
using Entitas.VisualDebugging.Unity;
using UnityEngine;
using EntityDebugger = Entitas.VisualDebugging.Unity.EntityBehaviour;

namespace FelineFellas
{
    public class ParenthoodDebugger
    {
        private readonly Dictionary<EntityID, EntityID> _processedEntities = new();

        private ContextObserverBehaviour ContextBehaviour { get; set; }

        public void Initialize()
        {
            var contexts = Object.FindObjectsByType<ContextObserverBehaviour>(FindObjectsSortMode.None);
            ContextBehaviour = contexts.FirstOrDefault(c => c.name.Contains(nameof(GameScope)));
        }

        public void OnUpdate()
        {
            if (ContextBehaviour is null)
                return;

            foreach (var entityDebugger in ContextBehaviour.GetComponentsInChildren<EntityDebugger>())
            {
                var entity = entityDebugger.entity;

                if (entity.isEnabled)
                    HandleEntity((Entity<GameScope>)entity, entityDebugger.transform);
            }
        }

        private void HandleEntity(Entity<GameScope> child, Transform childDebugger)
        {
            var entityID = child.ID();

            if (!child.IsAlive() || !child.TryGet<ChildOf, EntityID>(out var parentID))
            {
                if (_processedEntities.ContainsKey(entityID))
                {
                    childDebugger.SetParent(ContextBehaviour.transform);
                    _processedEntities.Remove(entityID);
                }

                return;
            }

            if (_processedEntities.TryGetValue(entityID, out var cachedParentID)
                && cachedParentID == parentID)
                return;

            var parentDebugger = FindParentDebugger(parentID);
            if (parentDebugger is not null)
            {
                childDebugger.SetParent(parentDebugger.transform);
                _processedEntities[entityID] = parentID;
            }
            else
            {
                childDebugger.SetParent(ContextBehaviour.transform);
                _processedEntities.Remove(entityID);
            }
        }

        private EntityDebugger FindParentDebugger(EntityID parentID)
            => ContextBehaviour.GetComponentsInChildren<EntityDebugger>()
                .FirstOrDefault(
                    debugger => debugger.entity.isEnabled
                        && parentID == ((Entity<GameScope>)debugger.entity).ID()
                );
    }
}