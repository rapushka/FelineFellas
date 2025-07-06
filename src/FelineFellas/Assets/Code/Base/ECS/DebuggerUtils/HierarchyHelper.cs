using Entitas.Generic;
using UnityEditor;
using UnityEngine;
using EntityDebugger = Entitas.VisualDebugging.Unity.EntityBehaviour;

namespace FelineFellas
{
    public static class HierarchyHelper
    {
        public static bool TryGetEntity(int instanceID, out Entity<GameScope> result)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            var entityBehaviour = gameObject != null ? gameObject.GetComponent<EntityDebugger>() : null;
            result = entityBehaviour != null ? entityBehaviour.entity as Entity<GameScope> : null;

            return result is not null;
        }

        public static Rect ButtonRect(Rect selectionRect, float width, float fromRight)
            => new(
                x: selectionRect.xMax - fromRight,
                y: selectionRect.y,
                width: width,
                height: selectionRect.height
            );
    }
}