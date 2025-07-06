using System;
using UnityEditor;
using UnityEngine;

namespace FelineFellas
{
    [InitializeOnLoad]
    public static class EntityHierarchyGUI
    {
        static EntityHierarchyGUI()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            if (!HierarchyHelper.TryGetEntity(instanceID, out var entity))
                return;

            if (entity.Has<Card>() && entity.Has<InHandIndex>())
            {
                Button(selectionRect, 60f, 90f, "Discard", Discard);
            }

            return;

            void Discard() => CardUtils.Discard(entity);
        }

        private static void Button(Rect rect, float width, float fromRight, string label, Action onClick)
        {
            rect = HierarchyHelper.ButtonRect(rect, width, fromRight);
            if (GUI.Button(rect, label))
                onClick.Invoke();
        }
    }
}