using UnityEditor;
using UnityEngine;

namespace FelineFellas.Editor
{
    [CustomPropertyDrawer(typeof(CardIDRef))]
    public class CardIDRefDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative(nameof(CardIDRef.Value));
            EditorGUI.PropertyField(rect, valueProperty, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative(nameof(CardIDRef.Value));
            return EditorGUI.GetPropertyHeight(valueProperty, label, true);
        }
    }
}